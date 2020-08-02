namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class RouteConfiguration:BaseEntity
    {
        public decimal? RtRefId { get; set; }
        public int? RtSeq { get; set; }
        public decimal? TopId { get; set; }
        public string CascadeForward { get; set; }
        public string CascadeReverse { get; set; }
        public int? DurationInMinutes { get; set; }

        public virtual RouteReference RtRef { get; set; }
    }
}
