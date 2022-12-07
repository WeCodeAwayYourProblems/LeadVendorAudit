using AuditCore;
using AuditInfrastructure;
using CliHelperClass;

namespace Infrastructure.VendorData.Driver;

public class VendorDataReader_Driver : IVendorDataRetrieval
{
   public VendorDataReader_Driver(ILeadDataParser parser, VendorRecord vendor, DateTime startDate, DateTime endDate, PageItems page, Credentials vendorSiteLogin, WebDriverManipulator driver)
   {
      StartDate = startDate;
      EndDate = endDate;
      Logins = vendorSiteLogin;
      WebD = driver;
      Page = page;
      Vendor = vendor;
      Parser = parser;
   }
   public ILeadDataParser Parser { get; }
   public VendorRecord Vendor { get; }
   public PageItems Page { get; }
   public DateTime EndDate { get; }
   public Credentials Logins { get; }
   public WebDriverManipulator WebD { get; private set; }
   public DateTime StartDate { get; }

   public IEnumerable<LeadItem> VendorData()
   {
      IEnumerable<LeadItem> records = new List<LeadItem>();
      bool success = false;
      while (!success)
      {
         bool opened = OpenVendorWebsite(Page.LoginPage.Url);
         bool loggedIn = LogIntoVendorSite(Logins);
         bool navigated = NavigateToAppropriatePage();
         bool extracted = ExtractHistoricalSalesData(StartDate, EndDate, out records);
         if (!opened || !loggedIn || !navigated || !extracted)
         {
            failureProtocols();
            continue;
         }
         success = true;
      }
      return records;

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
      bool clicked = WebD.ClickedOnElement(Page.LeadsPage.NextPageButton!, adjustWindow: false, multipleTries: true, shortenImplicitWaitBy: WebDriverManipulator.ImplicitWait);
      if (!clicked)
         WebD.NavigateToUrl(Page.LeadsPage.NextPageUrl!, usualWay: true);
      return true;
   }
   internal bool ExtractHistoricalSalesData(DateTime startingDate, DateTime endingDate, out IEnumerable<LeadItem> records)
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
         records = new List<LeadItem>();
         return false;
      }
      return true;
   }
}