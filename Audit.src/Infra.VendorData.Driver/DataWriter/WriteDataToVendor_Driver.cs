using AuditCore;
using AuditInfrastructure;
using CliHelperClass;
using OpenQA.Selenium.Chrome;

namespace Infrastructure.VendorData.Driver;

public class WriteDataToVendor_Driver : IVendorDataWriter
{
   public required string Url { get; init; }
   public required ChromeDriver Chrome { get; init; }
   public required WebDriverManipulator WebD { get; init; }
   public void WriteDataToVendor(IEnumerable<ILeadItem> leads)
   {
      throw new NotImplementedException();
   }
}