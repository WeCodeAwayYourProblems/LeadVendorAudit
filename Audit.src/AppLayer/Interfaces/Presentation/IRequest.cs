using CoreLogic;
namespace AppLayer;

public interface IRequest
{
   IVendorRecord Vendor(string vendorName);
   IEnumerable<IVendorRecord> Vendors(string[] vendorNames);
   DateTime StartDate { get; }
   DateTime EndDate { get; }
}