using AuditCore;
using CliHelperClass;

namespace Infrastructure.VendorData.Driver;

public class VendorDataRetrieval_Driver : IVendorDataRetrieval
{
   public VendorDataRetrieval_Driver(ILeadDataParser parser, VendorRecord vendor, DateTime startDate, DateTime endDate, PageItems page, Credentials vendorSiteLogin, WebDriverManipulator driver)
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
         if (!OpenVendorWebsite(Page.LoginPage.Url))
         {
            failureProtocols();
            continue;
         }
         if (!LogIntoVendorSite(Logins))
         {
            failureProtocols();
            continue;
         }
         if (!NavigateToAppropriatePage())
         {
            failureProtocols();
            continue;
         }
         if (!ExtractHistoricalSalesData(StartDate, EndDate, out records))
         {
            failureProtocols();
            continue;
         }
         success = true;
      }
      WebD.CloseChrome();
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
      return true;
   }
   internal bool ExtractHistoricalSalesData(DateTime startingDate, DateTime endingDate, out IEnumerable<LeadItem> records)
   {
      try
      {
         var leads = WebD.FindAllElements(Page.LeadsPage.LeadsElements, adjustWindow: false);

         records = Parser.ParseLeadText(
            leads.Where(item => item.Text.Length != 0)
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