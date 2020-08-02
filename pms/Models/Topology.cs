namespace pms.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Topology:BaseEntity
    {
        public Topology()
        {
            Navigation = new HashSet<Navigation>();
        }
        public decimal? ParentTopId { get; set; }
        public string TopCode { get; set; }
        public string TopName { get; set; }
        public string TopType { get; set; }
        public decimal PortId { get; set; }
        public string TerminalId { get; set; }

        public virtual ICollection<Navigation> Navigation { get; set; }
    }
}
