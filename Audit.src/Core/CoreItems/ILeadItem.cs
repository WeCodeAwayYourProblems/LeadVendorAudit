namespace AuditCore;
public interface ILeadItem
{
   public VendorRecord Vendor { get; set; }
   public IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public IPhoneNumber PhoneNumber { get; set; }
   public bool Billability { get; set; }
}
