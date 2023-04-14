using System.Text.RegularExpressions;
using CoreLogic;

namespace Infrastructure.VendorData.Driver;
public interface IVendorParser
{
   public void Parse(ICallLead call, Regex TimeDeterminer, Regex DateDeterminer, Regex LeadDetail_Region, Regex LeadDetail_Phone, Regex LeadDetail_Duration, string[] line);
}