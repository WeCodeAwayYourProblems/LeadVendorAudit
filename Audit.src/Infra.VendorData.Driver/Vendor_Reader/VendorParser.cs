using System.Text.RegularExpressions;
using AuditInfrastructure;
using CoreLogic;

namespace Infrastructure.VendorData.Driver;

public class VendorParser_Vendor_1 : IVendorParser
{
   public void Parse(ICallLead call, Regex TimeDeterminer, Regex DateDeterminer, Regex LeadDetail_Region, Regex LeadDetail_Phone, Regex LeadDetail_Duration, string[] line)
   {
      for (var ix = 0; ix < line.Length; ix++)
      {
         switch (line[ix])
         {
            // Set the Datetime
            case var i when TimeDeterminer.IsMatch(line[ix]) && DateDeterminer.IsMatch(line[ix + 1]):
               call.DateTime = DateTime.Parse(line[ix + 1] + " " + line[ix]);
               break;
            // Set the region
            case var i when LeadDetail_Region.IsMatch(line[ix]):
               call.CallerRegion = new Region(LeadDetail_Region.Match(line[ix]).Value);
               break;
            // Set the phone number
            case var i when LeadDetail_Phone.IsMatch(line[ix]):
               call.PhoneNumber = new PhoneNumber(LeadDetail_Phone.Match(line[ix]).Value);
               break;
            // Set call duration
            case var i when LeadDetail_Duration.IsMatch(line[ix]):
               call.Duration = TimeSpan.Parse(line[ix].Split(" ")[0]);
               break;
            default:
               break;
         }
      }
   }
}