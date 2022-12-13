using AuditCore;
using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver
{
   public interface ILeadDataParser
   {
      public List<ICallLead> ParseLeadText(IEnumerable<string> leadStrings);
   }
}