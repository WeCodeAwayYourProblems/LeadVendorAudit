namespace AuditCore;
public interface ILeadItem
{
   public IVendorRecord Vendor { get; init; }
   public IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public IPhoneNumber PhoneNumber { get; set; }
   public bool Billability { get; set; }
   public string BillabilityReasoning { get; set; }
}
