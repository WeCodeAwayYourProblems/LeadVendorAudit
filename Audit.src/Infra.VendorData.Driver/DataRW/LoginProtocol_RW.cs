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
   public required WebDriverManipulator WebD { get; init; }
   public ICredentials? Logins { get; set; }

   public virtual bool OpenVendorWebsite(string url)
   {
      WebD.StartChrome(loginURL: url, openSecondTab: false);
      if (WebD.GetCurrentUrl() != url)
         return false;
      return true;
   }
   public virtual bool LogIntoVendorSite(ICredentials logins)
   {
      // Enter Username
      WebD.SendKeysToElement(Page.LoginPage.User, logins.Username);
      // Enter Password
      WebD.SendKeysToElement(Page.LoginPage.Pass, logins.Password);
      // Try clicking the enter button
      try
      {
         PressEnterOnCredentialsPage();
      }
      catch
      { return false; }
      return true;

      // Local Function
      void PressEnterOnCredentialsPage()
      {
         try
         { WebD.ClickOnElement(Page.LoginPage.ContinueButton, adjustWindow: false); }
         // On failure, press enter on the password element
         catch
         { WebD.SendKeysToElement(Page.LoginPage.Pass, KeysEnum.Enter); }
      }
   }
   public virtual bool NavigateToAppropriatePage()
   {
      bool clicked = WebD.ClickedOnElement(Page.LeadsPage.NextPageButton!, adjustWindow: false, multipleTries: true, shortenImplicitWaitBy: WebDriverManipulator.ImplicitWait);
      if (!clicked)
         WebD.NavigateToUrl(Page.LeadsPage.NextPageUrl!, usualWay: true);
      return true;
   }
}