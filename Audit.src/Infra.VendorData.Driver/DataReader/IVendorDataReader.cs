using AuditInfrastructure;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataReader
{
   IEnumerable<LeadItem> VendorData();
}