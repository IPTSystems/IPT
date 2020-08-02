namespace pms.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TransportCall:BaseEntity
    {
        public TransportCall()
        {
            InternalMove = new HashSet<InternalMove>();
            ServiceRequest = new HashSet<ServiceRequest>();
        }
        public string CallGuid { get; set; }
        public string Imo { get; set; }
        public decimal? VesselId { get; set; }
        public decimal? VesselVersion { get; set; }
        public string PortCode { get; set; }
        public string Operator { get; set; }
        public string PortReference { get; set; }
        public string CustomReference { get; set; }
        public string RotationNumber { get; set; }
        public string CallStatus { get; set; }
        public DateTime Eta { get; set; }
        public DateTime? Ata { get; set; }
        public DateTime? Etd { get; set; }
        public DateTime? Atd { get; set; }
        public string VoyageIn { get; set; }
        public string VoyageOut { get; set; }
        public string ServiceIn { get; set; }
        public string ServiceOut { get; set; }
        public string PreviousPort { get; set; }
        public string PreviousTerminal { get; set; }
        public string NextPort { get; set; }
        public string NotResponsibleForOut { get; set; }
        public string ReadyForBilling { get; set; }

        public virtual ICollection<InternalMove> InternalMove { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
    }
}
