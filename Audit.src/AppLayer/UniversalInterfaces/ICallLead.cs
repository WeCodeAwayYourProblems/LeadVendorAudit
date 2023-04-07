namespace AuditCore;
public interface ICallLead
{
   public IVendorRecord Vendor { get; init; }
   public IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public IPhoneNumber PhoneNumber { get; set; }
   public bool Billability { get; set; }
   public string? BillabilityReasoning { get; set; }
   public string? Notes { get; set; }
}
