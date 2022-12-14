using System.Text.RegularExpressions;
using AuditCore;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;
public class LeadDataParser_Vendor_1 : ILeadDataParser
{
   public LeadDataParser_Vendor_1(string errorLogPath)
   {
      Delimiter = new Regex(@"\r\n|\n\r|\r|\n");
      TimeDeterminer = @"((a|p)m)|:";
      DateDeterminer = @"/";
      LeadDetail_Region = @"[M,m]arketplace -";
      LeadDetail_Phone = @"\(\d{3}\) ?\d{3}(-| )\d{4}";
      LeadDetail_Duration = @":.* \$";
      ErrorLogPath = errorLogPath;
   }
   // Public items
   public required string VendorName { get; init; }
   public string LeadDetail_Region { get; }
   public string LeadDetail_Phone { get; }
   public string LeadDetail_Duration { get; }
   public string ErrorLogPath { get; }

   // Private items
   private Regex Delimiter { get; }
   private string TimeDeterminer { get; }
   private string DateDeterminer { get; }

   public List<ICallLead> ParseLeadText(IEnumerable<string> leadStrings)
   {
      List<ICallLead> list = new();

      // Each line must be split by the delimiter
      List<string[]> parsedLines = new();
      foreach (var lead in leadStrings)
         parsedLines.Add(Delimiter.Split(lead));

      // Instantiate each item to be included in the lead item object
      VendorRecord vendor = new() { VendorName = VendorName };
      Region? region = default;
      PhoneNumber? number = default;
      DateTime date = default;
      TimeSpan duration = default;

      // These items are required so that we can tell whether the parsing assignment process works
      DateTime dateDefault = default;
      TimeSpan spanDefault = default;

      // Each item in each line must be parsed based on the vendor-specific parameters
      foreach (var line in parsedLines)
      {
         for (var item = 0; item < line.Length; item++) // All magic numbers here are targeted toward a specific
         {
            switch (line[item])
            {
               // Set the Datetime
               case var i when Regex.IsMatch(line[item], TimeDeterminer):
                  date = DateTime.Parse(line[item + 1] + line[item]);
                  break;
               // Set the Region item
               case var i when Regex.IsMatch(line[item], LeadDetail_Region):
                  region = new(line[item].Split(" ")[1]);
                  break;
               // Set the phone number item
               case var i when Regex.IsMatch(line[item], LeadDetail_Phone):
                  number = new(line[item]);
                  break;
               // Set TimeLine Duration
               case var i when Regex.IsMatch(line[item], LeadDetail_Duration):
                  duration = TimeSpan.Parse(line[item].Split(" ")[0]);
                  break;
            }
         }

         // If one of the parsing cases doesn't work, we don't want to add that item to the list, and we don't want to break the entire application
         if (region is null || date.Equals(dateDefault) || number is null || duration.Equals(spanDefault))
         {
            // We need to log the error so that we don't miss anything.
            VendorDataParserErrorLog(string.Join(" ", line));
            continue;
         }

         CallLead lead = new()
         {
            Vendor = vendor,
            CallerRegion = region!,
            DateTime = date,
            PhoneNumber = number!,
            CallDuration = duration
         };

         list.Add(lead);
      }
      return list;
   }
   public void VendorDataParserErrorLog(string erroneousLeadText) =>
      File.AppendAllText(ErrorLogPath, erroneousLeadText + "\n");
}