<Query Kind="Program">
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.Sqlite</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore.SqlServer</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Logging.Console</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Logging.Debug</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>System.Data.SQLite</NuGetReference>
  <Namespace>Microsoft.EntityFrameworkCore.ChangeTracking</Namespace>
  <Namespace>Microsoft.EntityFrameworkCore</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
  <Namespace>System.Data.Common</Namespace>
</Query>

void Main()
{
	var db = new SchoolContext();
	db.Courses.Add(new Course { CourseName = "MT", Description = "Mathematics" });
	var stud = new Student { Name = "Aravindh" };
	db.Students.Add(stud);
	db.Departments.Add(new Department { DeptName = "Maths", Division = "STEM" });
	db.Departments.Add(new Department { DeptName = "Science", Division = "STEM" });
	db.Departments.Add(new Department { DeptName = "English", Division = "LANG" });
	db.Departments.Add(new Department { DeptName = "French", Division = "LANG" });
	db.SaveChanges();

	Console.WriteLine("Check whether a student is added and a course is added");
	Console.WriteLine(db.Courses);
	Console.WriteLine(db.Students);
	Console.WriteLine(db.AuditLog);
	for (int i = 0; i < 3; i++)
	{
		stud.Name += "x";
		db.SaveChanges();
	}
	Console.WriteLine("Check student name ends with 3x");
	Console.WriteLine(db.Courses);
	Console.WriteLine(db.Students);
	Console.WriteLine(db.AuditLog);
	db.Courses.Add(new Course { CourseName = "SC", Description = "Science" });
	var cd = db.Courses.FirstOrDefault();
	cd.Description = "Changing description. Change of this field should not be audited";
	db.Courses.RemoveRange(db.Courses.Take(5));
	// var addedEntries = DisplayStates(db.ChangeTracker.Entries());
	db.SaveChanges();
	//foreach (var element in addedEntries)
	//{
	//	Console.WriteLine($"New '{element.GetType().Name}'-{JsonConvert.SerializeObject(element)} added with Id : {element.GetType().GetProperty("Id").GetValue(element)} ");
	//}
	Console.WriteLine("A course is deleted");
	Console.WriteLine(db.Courses);
	Console.WriteLine(db.Students);
	Console.WriteLine(db.AuditLog);
}

List<object> DisplayStates(IEnumerable<EntityEntry> entries)
{
	List<object> retVal = new List<object>();
	foreach (var entry in entries)
	{
		switch (entry.State)
		{
			case EntityState.Added:
				retVal.Add(entry.Entity);
				break;
			case EntityState.Deleted:
				Console.WriteLine($"Removed {entry.Entity.GetType().GetProperty("Id").GetValue(entry.Entity)} from '{entry.Entity.GetType().Name}' which had values {JsonConvert.SerializeObject(entry.Entity)} removed");
				break;
			case EntityState.Modified:
				foreach (var props in entry.Properties)
				{
					if (props.IsModified)
						Console.WriteLine($"{props.Metadata.Name} is modified from {props.OriginalValue} to {props.CurrentValue} ");
				}
				break;
		}
	}
	return retVal;
}

public class SchoolContext : DbContext
{
	private List<Auditable> _auditable = new List<Auditable>();
	private List<string> _EntityToAudit ;
	public SchoolContext() : base()
	{
		_auditable.Add(new Auditable { Name = "Student", Columns = "Name".Split(',') });
		_auditable.Add(new Auditable { Name = "Course", Columns = "CourseName".Split(',') });
		
		_EntityToAudit = _auditable.Select(_ => _.Name).ToList();
		
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(@"DataSource=EFChangeTrackingDemystified.db");
		// optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EFDemystified.db;Trusted_Connection=True");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var allEntities = modelBuilder.Model.GetEntityTypes().Where(_ => _.Name != "AuditLog");


		foreach (var entity in allEntities.Where(e => e.Name != "AuditLog"))
		{
			entity.AddProperty("CreateDT", typeof(DateTime));
			entity.AddProperty("LUDT", typeof(DateTime));
		}
		modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
	}

	public override int SaveChanges()
	{
		List<object> addedEntries = new List<object>();
		var entryShadowProps = ChangeTracker
			.Entries()
			.Where(e =>
					e.State == EntityState.Added
					|| e.State == EntityState.Modified
					|| e.State == EntityState.Deleted);

		foreach (var entityEntry in entryShadowProps)
		{
			List<PropTrack> chgs = new List<PropTrack>();
			entityEntry.Property("LUDT").CurrentValue = DateTime.Now;
			switch (entityEntry.State)
			{
				case EntityState.Added:
					entityEntry.Property("CreateDT").CurrentValue = DateTime.Now;
					addedEntries.Add(entityEntry.Entity);
					break;
				case EntityState.Modified:
					foreach (var props in entityEntry.Properties.OrderBy(_ => _.Metadata.Name))
					{
						if (props.IsModified && 
						_EntityToAudit.Contains(entityEntry.Metadata.GetDefaultTableName()) && _auditable.FirstOrDefault(_ => _.Name == entityEntry.Metadata.GetDefaultTableName()).Columns.Contains(props.Metadata.Name))
									chgs.Add(new PropTrack { Field = props.Metadata.Name, Changes = new Changed { From = props.OriginalValue.ToString(), To = props.CurrentValue.ToString() } });
					}
					check_and_add_audit(entityEntry.Metadata.GetDefaultTableName(), new AuditLog { ITPOperation = "Modified", ITPEntityId = entityEntry.CurrentValues.GetValue<int>("Id"), ChangedValueJson = JsonConvert.SerializeObject(chgs) });
					break;
				case EntityState.Deleted:
					check_and_add_audit(entityEntry.Metadata.GetDefaultTableName(), new AuditLog { ITPOperation = "Deleted", ITPEntityId = entityEntry.CurrentValues.GetValue<int>("Id"), ChangedValueJson = JsonConvert.SerializeObject(entityEntry.Entity) });
					break;
			}
		}
		int retVal = base.SaveChanges();
		foreach (var element in addedEntries)
		{
			check_and_add_audit(element.GetType().Name, new AuditLog { ITPOperation = "Added", ITPEntityId = Convert.ToInt64(element.GetType().GetProperty("Id").GetValue(element)), ChangedValueJson = JsonConvert.SerializeObject(element) });
		}
		base.SaveChanges();
		return retVal;
	}

	

	private void check_and_add_audit(string TName, AuditLog auditLog)
	{
		if (_EntityToAudit.Contains(TName))
		{
			auditLog.ITPEntity = TName;
			Add(auditLog);
		}
	}

	public DbSet<Student> Students { get; set; }
	public DbSet<Course> Courses { get; set; }
	public DbSet<Department> Departments { get; set; }
	public DbSet<StudentCourse> StudentCourses { get; set; }
	public DbSet<AuditLog> AuditLog { get; set; }
}

public class BaseEntity
{
	[Key]
	public int Id { get; set; }

	[ConcurrencyCheckAttribute]
	public int Version { get; set; }
}

public class Student : BaseEntity
{
	public string Name { get; set; }
	[JsonIgnore]
	public IList<StudentCourse> StudentCourses { get; set; }
}

public class Course : BaseEntity
{
	public string CourseName { get; set; }
	public string Description { get; set; }
	[JsonIgnore]
	public IList<StudentCourse> StudentCourses { get; set; }
}

public class Department : BaseEntity
{
	public string DeptName { get; set; }
	public string Division { get; set; }
}

public class StudentCourse
{
	public int StudentId { get; set; }
	[JsonIgnore]
	public Student Student { get; set; }

	public int CourseId { get; set; }
	[JsonIgnore]
	public Course Course { get; set; }
}

public class AuditLog : BaseEntity
{
	public long ITPEntityId { get; set; }
	public string ITPEntity { get; set; }
	public string ITPOperation { get; set; }
	public string ChangedValueJson { get; set; }
}

public class PropTrack
{
	public string Field { get; set; }
	public Changed Changes { get; set; }
}

public class Changed
{
	public string From { get; set; }
	public string To { get; set; }
}

public class Auditable
{
	public string Name { get; set; }
	public string[] Columns { get; set; }
}