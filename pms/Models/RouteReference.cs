namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class RouteReference:BaseEntity
    {

        public string Name { get; set; }
        public string PortCode { get; set; }
        public decimal FromTopId { get; set; }
        public decimal ToTopId { get; set; }
        public string IsActive { get; set; }
        public string IsBidirectional { get; set; }

    }
}