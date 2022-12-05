namespace AuditCore;
public interface ILeadItem
{
   public VendorRecord Vendor { get; set; }
   public Region CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public PhoneNumber PhoneNumber { get; set; }
   public bool Billability { get; set; }
}
