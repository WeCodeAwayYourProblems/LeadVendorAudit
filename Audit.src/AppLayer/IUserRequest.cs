using CoreLogic;
namespace AppLayer;

public interface IUserRequest
{
   public Dictionary<IVendorRecord, IDateRange>? VendorStartEnd { get; set; }
}
