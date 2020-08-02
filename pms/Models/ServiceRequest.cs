namespace pms.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class ServiceRequest:BaseEntity
    {
        public string SvcCode { get; set; }
        public decimal? CallId { get; set; }
        public decimal? MoveId { get; set; }
        public decimal? VesselId { get; set; }
        public string VesselName { get; set; }
        public string PortCode { get; set; }
        public decimal SvcConfigId { get; set; }
        public decimal? TopLoc { get; set; }
        public decimal Requestor { get; set; }
        public DateTime? SvcReqFrom { get; set; }
        public DateTime? SvcReqUntil { get; set; }
        public decimal? ReqQty { get; set; }
        public string ReqQtyUnit { get; set; }
        public string RequestorComments { get; set; }
        public decimal? SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public DateTime? PlanStart { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? PlanEnd { get; set; }
        public DateTime? ActualEnd { get; set; }
        public decimal? DlvdQty { get; set; }
        public string DlvdQtyUnit { get; set; }
        public string SupplierComments { get; set; }
        public string SvcStatus { get; set; }

        public virtual TransportCall Call { get; set; }
        public virtual InternalMove Move { get; set; }
    }
}
