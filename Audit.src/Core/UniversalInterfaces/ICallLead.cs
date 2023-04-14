namespace CoreLogic;
public interface ICallLead : ILead
{
   public IVendorRecord Vendor { get; init; }
   public IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public IPhoneNumber PhoneNumber { get; set; }
   public TimeSpan Duration { get; set; }
}
