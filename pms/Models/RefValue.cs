namespace pms.Models
{
    public partial class RefValue : BaseEntity
    {
        public string RVName { get; set; }
        public string RVValue { get; set; }
        public string RVTenant { get; set; }
        public string RVAppScope { get; set; }
        public string RVDescription { get; set; }
        public string IsProtected { get; set; }
    }
}