using CoreLogic;
namespace AppLayer;

public class UserRequest : IUserRequest
{
   public Dictionary<IVendorRecord, IDateRange>? VendorStartEnd { get; set; }
}