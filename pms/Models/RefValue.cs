namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;


    public partial class RefValue:BaseEntity
    {
        public string RefName { get; set; }
        public string RefValue1 { get; set; }
        public string RefValTenant { get; set; }
        public string RefValAppScope { get; set; }
        public string RefValDescription { get; set; }
        public string IsProtected { get; set; }
    }
}
