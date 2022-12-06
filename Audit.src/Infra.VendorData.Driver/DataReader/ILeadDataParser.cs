using System.Text.RegularExpressions;

namespace Infrastructure.VendorData.Driver
{
   public interface ILeadDataParser
   {
      public Regex Delimiter { get; set; }
      public List<LeadItem> ParseLeadText(IEnumerable<string> leadStrings);
   }
}