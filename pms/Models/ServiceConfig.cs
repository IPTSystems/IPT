namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class ServiceConfig:BaseEntity
    {
        public string SvcCode { get; set; }
        public string PortCode { get; set; }
        public string OncePerMove { get; set; }
        public string DurationBased { get; set; }
        public string SvcStatus { get; set; }
        public string QuantityBased { get; set; }
        public string PersonRequired { get; set; }
        public string EquipRequired { get; set; }
        public string SupplierGroup { get; set; }
        public string SupplierRequired { get; set; }
        public string SvcOnInMove { get; set; }
        public string SvcOnOutMove { get; set; }
        public string SvcOnShiftMove { get; set; }
        public string DefaultService { get; set; }
        public string Role1 { get; set; }
        public string Role2 { get; set; }
        public string Role3 { get; set; }
        public string EquipType1 { get; set; }
        public string EquipType2 { get; set; }
        public string EquipType3 { get; set; }
    }
}
