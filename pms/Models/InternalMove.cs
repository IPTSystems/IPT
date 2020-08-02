namespace pms.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class InternalMove : BaseEntity
    {
        public InternalMove()
        {
            Navigation = new HashSet<Navigation>();
            ServiceRequest = new HashSet<ServiceRequest>();
        }
        public decimal CallId { get; set; }
        public decimal Sequence { get; set; }
        public string MoveType { get; set; }
        public string MoveSubType { get; set; }
        public string MoveStatus { get; set; }
        public string AgnentRequest { get; set; }
        public string FromLoc { get; set; }
        public string ToLoc { get; set; }
        public string BerthingSide { get; set; }
        public DateTime Eet { get; set; }
        public DateTime? Aet { get; set; }
        public DateTime Est { get; set; }
        public DateTime? Ast { get; set; }
        public decimal? DraftFore { get; set; }
        public decimal? DraftAft { get; set; }
        public decimal? DraftAir { get; set; }
        public string Agent { get; set; }
        public DateTime? HandOverAt { get; set; }
        public string TerminalCode { get; set; }

        public virtual TransportCall Call { get; set; }
        public virtual ICollection<Navigation> Navigation { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
    }
}
