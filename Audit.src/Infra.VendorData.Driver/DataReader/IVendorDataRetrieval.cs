using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataRetrieval
{
   IEnumerable<LeadItem> VendorData();
}