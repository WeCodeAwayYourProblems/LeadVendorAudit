using AuditCore;
using CliHelperClass;

namespace Infrastructure.VendorData.Driver;

public class VendorDataRetrieval_WebDriver : IVendorDataRetrieval
{
   public VendorDataRetrieval_WebDriver(DateTime startDate, DateTime endDate, PageItems page, Credentials vendorSiteLogin, WebDriverManipulator driver)
   {
      StartDate = startDate;
      EndDate = endDate;
      Logins = vendorSiteLogin;
      WebD = driver;
      Page = page;
   }
   public PageItems Page { get; }
   public DateTime EndDate { get; }
   public Credentials Logins { get; }
   public WebDriverManipulator WebD { get; private set; }
   public DateTime StartDate { get; }

   public IEnumerable<VendorRecord> VendorData()
   {
      bool success = false;
      while (!success)
      {
         if (!OpenVendorWebsite(Page.LoginPage.Url))
         {
            failureProtocols();
            continue;
         }
         if (!LogIntoVendorSite(Logins))
            continue;
         if (!NavigateToAppropriatePage())
            continue;
         success = true;
      }
      WebD.CloseChrome();
      return ExtractHistoricalSalesData(StartDate, EndDate);

      void failureProtocols()
      {
         WebD.CloseChrome();
      }
   }
   internal bool OpenVendorWebsite(string url)
   {
      WebD.StartChrome(loginURL: url, openSecondTab: false);
      if (WebD.GetCurrentUrl() != url)
         return false;
      return true;
   }
   internal bool LogIntoVendorSite(Credentials creds)
   {
      WebD.SendKeysToElement(Page.LoginPage.User, creds.Username);
      WebD.SendKeysToElement(Page.LoginPage.Pass, creds.Password);
      try
      { WebD.ClickOnElement(Page.LoginPage.ContinueButton, adjustWindow: false); }
      catch
      { WebD.SendKeysToElement(Page.LoginPage.Pass, KeysEnum.Enter); }
      return true;
   }
   internal bool NavigateToAppropriatePage()
   {
      return true;
   }
   internal IEnumerable<VendorRecord> ExtractHistoricalSalesData(DateTime startingDate, DateTime endingDate)
   {
      var leads = WebD.FindAllElements(Page.LeadsPage.LeadsElements, adjustWindow: false);
      return new List<VendorRecord>();
   }
}