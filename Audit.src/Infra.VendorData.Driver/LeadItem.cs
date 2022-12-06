using AuditCore;
namespace Infrastructure.VendorData.Driver;

public class LeadItem : ILeadItem
{
   public LeadItem(VendorRecord vendor, Region callOrigin, DateTime dateTime, PhoneNumber phoneNumber, TimeSpan duration)
   {
      Vendor = vendor;
      CallerRegion = callOrigin;
      DateTime = dateTime;
      PhoneNumber = phoneNumber;
      CallDuration = duration;
   }
   public VendorRecord Vendor { get; set; }
   public Region CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public PhoneNumber PhoneNumber { get; set; }
   public TimeSpan CallDuration { get; set; }
   public bool Billability { get; set; }


}