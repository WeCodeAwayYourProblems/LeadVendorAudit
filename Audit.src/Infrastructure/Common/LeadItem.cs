using AuditCore;
namespace AuditInfrastructure;

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
   public IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public IPhoneNumber PhoneNumber { get; set; }
   public TimeSpan CallDuration { get; set; }
   public bool Billability { get; set; }


}