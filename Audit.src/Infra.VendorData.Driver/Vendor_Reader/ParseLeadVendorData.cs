using System.Text.RegularExpressions;
using CoreLogic;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;
public class ParseLeadVendorData : ILeadDataParser
{
   // These parameter names cannot change because they are copied in another file
   public ParseLeadVendorData(Regex lineDelimiter, Regex timeRegex, Regex dateRegex, Regex leadRegionRegex, Regex leadPhoneRegex, Regex leadDurationRegex, string vendorName, string errorLogPath_Absolute_csv, VendorParser parser)
   {
      Delimiter = lineDelimiter;
      TimeDeterminer = timeRegex;
      DateDeterminer = dateRegex;
      LeadDetail_Region = leadRegionRegex;
      LeadDetail_Phone = leadPhoneRegex;
      LeadDetail_Duration = leadDurationRegex;
      ErrLogPath = errorLogPath_Absolute_csv;
      Vendor = vendorName;
      Parser = parser;
   }
   // Public properties
   public string Vendor { get; }
   public Regex LeadDetail_Region { get; }
   public Regex LeadDetail_Phone { get; }
   public Regex LeadDetail_Duration { get; }
   public string ErrLogPath { get; }
   public Regex Delimiter { get; }
   public Regex TimeDeterminer { get; }
   public Regex DateDeterminer { get; }

   // Object properties
   public VendorParser Parser { get; }

   public List<ICallLead> ParseLeadText(IEnumerable<string> leadStrings)
   {
      List<ICallLead> list = new();

      // Each line must be split by the delimiter
      List<string[]> splitLines = new();
      foreach (var lead in leadStrings)
         splitLines.Add(Delimiter.Split(lead));

      // Instantiate each item to be included in the lead item object
      VendorRecord vendor = new() { VendorName = Vendor };
      Region? region = default;
      PhoneNumber? number = default;
      DateTime date = default;
      TimeSpan duration = default;

      // These items are required so that we can tell whether the parsing assignment process works
      DateTime dateDefault = default;
      TimeSpan spanDefault = default;

      // Each item in each line must be parsed based on the vendor-specific parameters
      foreach (var line in splitLines)
      {
         Parser.ParseVendor1(ref region, ref number, ref date, ref duration, TimeDeterminer, DateDeterminer, LeadDetail_Region, LeadDetail_Phone, LeadDetail_Duration, line);

         if (region is null || number is null || date.Equals(dateDefault) || duration.Equals(spanDefault))
         {
            // If the parsing doesn't work, we need to log the error so that we can make adjustments to this algorithm.
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
      File.AppendAllText(ErrLogPath, erroneousLeadText + "," + ErrorLogMessage + "\n");
   private string ErrorLogMessage = $"The lead data from the website changed and the parser no longer works correctly. Location: {nameof(ParseLeadText)} method in the {nameof(VendorParser)} class.";
}