using AuditCore;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataReader
{
   IEnumerable<ICallLead> VendorData();
}