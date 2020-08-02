namespace pms.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class StakeHolder:BaseEntity
    {
        public string TenantCode { get; set; }
        public string ShortCode { get; set; }
        public string StakeHolderType { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string ZipOrPinCode { get; set; }
        public string Place { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Mobille { get; set; }
        public string EmailId { get; set; }
        public string WebSite { get; set; }
        public string Language { get; set; }
        public string StakeHolderStatus { get; set; }
        public string Vatnumber { get; set; }
        public string TradeNumber { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
