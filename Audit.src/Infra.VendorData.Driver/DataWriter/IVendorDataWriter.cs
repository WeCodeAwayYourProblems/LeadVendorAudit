using AuditCore;

namespace Infrastructure.VendorData.Driver;

public interface IVendorDataWriter
{
   public void WriteDataToVendor(IEnumerable<ILeadItem> leads);
}