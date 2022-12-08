using AuditCore;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver
{
   public interface ILeadDataParser
   {
      public List<ILeadItem> ParseLeadText(IEnumerable<string> leadStrings);
   }
}