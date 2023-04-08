using System.Text.RegularExpressions;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;

public class VendorParser
{
   public void ParseVendor1(ref Region? region, ref PhoneNumber? number, ref DateTime date, ref TimeSpan duration, Regex TimeDeterminer, Regex DateDeterminer, Regex LeadDetail_Region, Regex LeadDetail_Phone, Regex LeadDetail_Duration, string[] line)
   {
      for (var ix = 0; ix < line.Length; ix++)
      {
         switch (line[ix])
         {
            // Set the Datetime
            case var i when TimeDeterminer.IsMatch(line[ix]) && DateDeterminer.IsMatch(line[ix + 1]):
               date = DateTime.Parse(line[ix + 1] + " " + line[ix]);
               break;
            // Set the region
            case var i when LeadDetail_Region.IsMatch(line[ix]):
               region = new(LeadDetail_Region.Match(line[ix]).Value);
               break;
            // Set the phone number
            case var i when LeadDetail_Phone.IsMatch(line[ix]):
               number = new(LeadDetail_Phone.Match(line[ix]).Value);
               break;
            // Set call duration
            case var i when LeadDetail_Duration.IsMatch(line[ix]):
               duration = TimeSpan.Parse(line[ix].Split(" ")[0]);
               break;
         }
      }
   }
}