using AuditCore;
using AuditInfrastructure;
using OpenQA.Selenium.Chrome;

namespace Infrastructure.VendorData.Driver;

public class ReadVendorData_Driver : LoginProtocol_RW, IVendorDataReader
{
   public required ILeadDataParser Parser { get; init; }

   public ChromeDriver AccessChromeDriver() =>
      WebD.Chrome!;
   public IEnumerable<ICallLead> VendorData()
   {
      IEnumerable<ICallLead> records = new List<ICallLead>();
      bool success = false;
      while (!success)
      {
         bool opened = OpenVendorWebsite(Page.LoginPage.Url);
         bool loggedIn = LogIntoVendorSite(Logins);
         bool navigated = NavigateToAppropriatePage();
         bool extracted = ExtractHistoricalSalesData(StartDate, EndDate, out records);
         if (!opened || !loggedIn || !navigated || !extracted)
         {
            failureProtocol();
            continue;
         }
         success = true;
      }
      return records;

      void failureProtocol()
      {
         WebD.CloseChrome();
      }
   }
   public bool ExtractHistoricalSalesData(DateTime startingDate, DateTime endingDate, out IEnumerable<ICallLead> records)
   {
      try
      {
         var leads = WebD.FindAllElements(Page.LeadsPage.LeadsElements!, adjustWindow: false);

         records = Parser.ParseLeadText(
            leads
            .Where(item => item.Text.Length != 0)
            .Select(item => item.Text));
      }
      catch
      {
         records = new List<ICallLead>();
         return false;
      }
      return true;
   }
}