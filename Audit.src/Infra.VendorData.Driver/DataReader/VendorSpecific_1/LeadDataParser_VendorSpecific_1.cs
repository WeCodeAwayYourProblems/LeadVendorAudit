using System.Text.RegularExpressions;
using AuditCore;

namespace Infrastructure.VendorData.Driver
{
   public class LeadDataParser_VendorSpecific_1 : ILeadDataParser
   {
      public LeadDataParser_VendorSpecific_1(string vendorName, string lineDelimiterRegexText, string timeDeterminerRegexText, string dateDeterminerRegexText, string regionRegexText, string phoneNumberRegexText, string durationRegexText)
      {
         // These are determined based on what the vendor website needs
         VendorName = vendorName;
         Delimiter = new Regex(lineDelimiterRegexText);
         TimeDeterminer = timeDeterminerRegexText;
         DateDeterminer = dateDeterminerRegexText;
         LeadDetail_Region = regionRegexText;
         LeadDetail_Phone = phoneNumberRegexText;
         LeadDetail_Duration = durationRegexText;
      }
      public Regex Delimiter { get; set; }
      public string VendorName { get; }
      private string TimeDeterminer { get; }
      private string DateDeterminer { get; }
      public string LeadDetail_Region { get; }
      public string LeadDetail_Phone { get; }
      public string LeadDetail_Duration { get; }
      public List<LeadItem> ParseLeadText(IEnumerable<string> leadStrings)
      {
         List<LeadItem> list = new();

         // Each line must be split by the delimiter
         List<string[]> parsedLines = new();
         foreach (var lead in leadStrings)
            parsedLines.Add(Delimiter.Split(lead));

         // Instantiate each item to be included in the lead item object
         VendorRecord vendor = new(VendorName);
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

            // If one of the parsing doesn't work, we don't want to add that item to the list, and we don't want to break the entire application
            if (region is null || date.Equals(dateDefault) || number is null || duration.Equals(spanDefault))
            {
               // We need to log the error so that we don't miss anything.
               continue;
            }

            list.Add(new LeadItem(vendor, region!, date, number!, duration));
         }
         return list;
      }
   }
}