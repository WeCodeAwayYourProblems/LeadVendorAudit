using AuditCore;
using AuditInfrastructure;
using CliHelperClass;
using OpenQA.Selenium.Chrome;

namespace Infrastructure.VendorData.Driver;

public class ReadVendorData_Driver : IVendorDataReader
{
   public required ILeadDataParser Parser { get; init; }
   public required IVendorRecord Vendor { get; init; }
   public required IPageItems Page { get; init; }
   public required ICredentials Logins { get; init; }
   public required DateTime StartDate { get; init; }
   public required DateTime EndDate { get; init; }
   public required WebDriverManipulator WebD { get; init; }

   public ChromeDriver AccessChromeDriver() =>
      WebD.Chrome!;
   public IEnumerable<ILeadItem> VendorData()
   {
      IEnumerable<ILeadItem> records = new List<ILeadItem>();
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
   internal bool OpenVendorWebsite(string url)
   {
      WebD.StartChrome(loginURL: url, openSecondTab: false);
      if (WebD.GetCurrentUrl() != url)
         return false;
      return true;
   }
   internal bool LogIntoVendorSite(ICredentials creds)
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
   internal bool ExtractHistoricalSalesData(DateTime startingDate, DateTime endingDate, out IEnumerable<ILeadItem> records)
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