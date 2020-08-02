namespace pms.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class RefDefinition:BaseEntity
    {
        public string RefName { get; set; }
        public string RefDescription { get; set; }
        [DefaultValue('Y')]
        public string IsSystem { get; set; }
        public string RefDataType { get; set; }
        public int RefDataLength { get; set; }
        public int RefDataPrecision { get; set; }
    }
}