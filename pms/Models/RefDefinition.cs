namespace pms.Models
{
    using System.ComponentModel;

    public partial class RefDefinition : BaseEntity
    {
        public string RefName { get; set; }
        public string RefDescription { get; set; }
        [DefaultValue('Y')]
        public string IsSystem { get; set; }
        [DefaultValue("STR")]
        public string RefDataType { get; set; }
        [DefaultValue(10)]
        public int RefDataLength { get; set; }
        [DefaultValue(0)]
        public int RefDataPrecision { get; set; }
    }
}