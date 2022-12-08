using AuditCore;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataReader
{
   IEnumerable<ILeadItem> VendorData();
}