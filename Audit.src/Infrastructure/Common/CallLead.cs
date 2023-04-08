using CoreLogic;
namespace AuditInfrastructure;

public class CallLead : ICallLead
{
   public required IVendorRecord Vendor { get; init; }
   public required IRegion CallerRegion { get; set; }
   public DateTime DateTime { get; set; }
   public required IPhoneNumber PhoneNumber { get; set; }
   public TimeSpan CallDuration { get; set; }
   public Billability? Billability { get; set; }
   public string? Notes { get; set; }
}