namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Vessel:BaseEntity
    {
        public string VesselName { get; set; }
        public string Imo { get; set; }
        public string CallSign { get; set; }
        public string Nationality { get; set; }
        public string VesselType { get; set; }
        public string VesselSubType { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? NetTonnage { get; set; }
        public decimal? GrossTonnage { get; set; }
        public decimal? Draft { get; set; }
        public decimal? DeadWeight { get; set; }
        public string VesselStatus { get; set; }
    }
}
