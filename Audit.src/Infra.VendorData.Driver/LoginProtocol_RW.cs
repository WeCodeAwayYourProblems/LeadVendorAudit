using AuditCore;
using AuditInfrastructure;
using CliHelperClass;

namespace Infrastructure.VendorData.Driver;

public abstract class LoginProtocol_RW
{
   public required DateTime StartDate { get; init; }
   public required DateTime EndDate { get; init; }
   public required IVendorRecord Vendor { get; init; }
   public required IPageItems Page { get; init; }
   public required ICredentials Logins { get; init; }
   public required WebDriverManipulator WebD { get; init; }

   public virtual bool OpenVendorWebsite(string url)
   {
      WebD.StartChrome(loginURL: url, openSecondTab: false);
      if (WebD.GetCurrentUrl() != url)
         return false;
      return true;
   }
   public virtual bool LogIntoVendorSite(ICredentials creds)
   {
      WebD.SendKeysToElement(Page.LoginPage.User, creds.Username);
      WebD.SendKeysToElement(Page.LoginPage.Pass, creds.Password);
      try
      { WebD.ClickOnElement(Page.LoginPage.ContinueButton, adjustWindow: false); }
      catch
      { WebD.SendKeysToElement(Page.LoginPage.Pass, KeysEnum.Enter); }
      return true;
   }
   public virtual bool NavigateToAppropriatePage()
   {
      bool clicked = WebD.ClickedOnElement(Page.LeadsPage.NextPageButton!, adjustWindow: false, multipleTries: true, shortenImplicitWaitBy: WebDriverManipulator.ImplicitWait);
      if (!clicked)
         WebD.NavigateToUrl(Page.LeadsPage.NextPageUrl!, usualWay: true);
      return true;
   }
}