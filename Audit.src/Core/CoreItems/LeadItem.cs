namespace AuditCore;

public class LeadItem : ILeadItem
{
   public LeadItem(VendorRecord vendor, Region callOrigin, DateTime dateTime, PhoneNumber phoneNumber)
   {
      Vendor = vendor;
      CallerRegion = callOrigin;
      DateTime = dateTime;
      PhoneNumber = phoneNumber;
   }
   public VendorRecord Vendor { get; set; }
   public Region CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public PhoneNumber PhoneNumber { get; set; }
   public bool Billability { get; set; }

}