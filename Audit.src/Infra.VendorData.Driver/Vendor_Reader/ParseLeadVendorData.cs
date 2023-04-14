using System.Text.RegularExpressions;
using CoreLogic;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;
public class ParseCallLeadData : ILeadDataParser
{
   /* These parameter names cannot change because they are copied in another file:
      Regexes, name of the vendor */
   public ParseCallLeadData(Regex lineDelimiter, Regex timeRegex, Regex dateRegex, Regex leadRegionRegex, Regex leadPhoneRegex, Regex leadDurationRegex, string vendorName, string errorLogPath_Absolute_csv, IVendorParser parser)
   {
      Delimiter = lineDelimiter;
      TimeDeterminer = timeRegex;
      DateDeterminer = dateRegex;
      LeadRegion = leadRegionRegex;
      LeadPhone = leadPhoneRegex;
      LeadDuration = leadDurationRegex;
      ErrLogPath = errorLogPath_Absolute_csv;
      Vendor = vendorName;
      Parser = parser;
   }
   // Public properties
   public string Vendor { get; }
   public Regex LeadRegion { get; }
   public Regex LeadPhone { get; }
   public Regex LeadDuration { get; }
   public string ErrLogPath { get; }
   public Regex Delimiter { get; }
   public Regex TimeDeterminer { get; }
   public Regex DateDeterminer { get; }

   // Object properties
   public IVendorParser Parser { get; }

   public List<ICallLead> ParseLeadText(IEnumerable<string> leadStrings)
   {
      List<ICallLead> list = new();

      // Each line must be split by the delimiter
      List<string[]> splitLines = new();
      foreach (var lead in leadStrings)
         splitLines.Add(Delimiter.Split(lead));

      // Instantiate each item to be included in the lead item object
      // These items are required so that we can tell whether the parsing assignment process works
      VendorRecord vendor = new() { VendorName = Vendor };
      Region region = new("false");
      Region regionComparer = new("false");
      PhoneNumber number = new(1234567890);
      PhoneNumber numberComparer = new(1234567890);
      DateTime date = default;
      DateTime dateDefault = default;
      TimeSpan duration = default;
      TimeSpan durationDefault = default;

      // Each item in each line must be parsed based on the vendor-specific parameters
      foreach (var line in splitLines)
      {
         // Set call defaults so the parser can use the lead object
         CallLead lead = new()
         {
            Vendor = vendor,
            CallerRegion = region!,
            PhoneNumber = number!,
            DateTime = date,
            Duration = duration
         };
         Parser.Parse(lead, TimeDeterminer, DateDeterminer, LeadRegion, LeadPhone, LeadDuration, line);

         if (lead.CallerRegion.Equals(regionComparer) || lead.PhoneNumber.Equals(numberComparer) || lead.DateTime.Equals(dateDefault) || lead.Duration.Equals(durationDefault))
         {
            // If the parsing doesn't work, we need to log the error so that we can make adjustments to the parsing algorithm.
            VendorDataParserErrorLog(string.Join(" ", line), Vendor);
            continue;
         }

         list.Add(lead);
      }
      return list;
   }

   public void VendorDataParserErrorLog(string erroneousLeadText, string vendorName) =>
      File.AppendAllText(ErrLogPath, "Error: " + ErrorLogMessage + "," + "Vendor: " + vendorName + "," + "Erroneous Lead text: " + erroneousLeadText + "\n");
   private string ErrorLogMessage = $"The lead data from the website changed and the parser no longer works correctly. Location: {nameof(ParseLeadText)} method in the {nameof(IVendorParser)} class.";
}