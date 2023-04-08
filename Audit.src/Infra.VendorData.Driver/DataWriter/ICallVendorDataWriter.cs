using CoreLogic;

namespace Infrastructure.VendorData.Driver;

public interface ICallVendorDataWriter
{
   public bool WriteDataToVendor(IEnumerable<ICallLead> leads);
}