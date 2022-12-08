using AuditCore;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataWriter
{
   public bool WriteDataToVendor(IEnumerable<ILeadItem> leads);
}