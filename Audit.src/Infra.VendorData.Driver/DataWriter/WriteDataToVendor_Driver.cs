using AuditCore;
using OpenQA.Selenium.Chrome;

namespace Infrastructure.VendorData.Driver;

public class WriteDataToVendor_Driver : LoginProtocol_RW, IVendorDataWriter
{
   public bool WriteDataToVendor(IEnumerable<ILeadItem> leads)
   {
      bool success = false;
      while (!success)
      {
         bool opened = OpenVendorWebsite(Page.LoginPage.Url);
         bool loggedIn = LogIntoVendorSite(Logins);
         bool navigated = NavigateToAppropriatePage();
         bool entered = EnterFormulatedSalesData(StartDate, EndDate, leads);
         if (!opened || !loggedIn || !navigated || !entered)
         {
            failureProtocol();
            continue;
         }
      }
      return true;
      void failureProtocol()
      {
         WebD.CloseChrome();
      }
   }

   public bool EnterFormulatedSalesData(DateTime startDate, DateTime endDate, IEnumerable<ILeadItem> leads)
   {
      throw new NotImplementedException();
   }
}