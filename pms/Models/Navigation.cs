namespace pms.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Navigation:BaseEntity
    {
        [ForeignKey("InternalMove")]
        public decimal MoveId { get; set; }
        public decimal TopId { get; set; }
        public decimal NavSequence { get; set; }
        public string IsCascadeEta { get; set; }
        public DateTime Eta { get; set; }
        public DateTime? Ata { get; set; }
        public string AnchorDown { get; set; }
        public DateTime? ExpectedAnchorUpAt { get; set; }
        public DateTime? ActualAnchorUpAt { get; set; }

        public virtual InternalMove Move { get; set; }
        public virtual Topology Top { get; set; }
    }
}
