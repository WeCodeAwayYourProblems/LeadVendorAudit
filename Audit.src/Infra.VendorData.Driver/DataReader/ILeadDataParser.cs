namespace Infrastructure.VendorData.Driver
{
   public interface ILeadDataParser
   {
      public List<LeadItem> ParseLeadText(IEnumerable<string> leadStrings);
   }
}