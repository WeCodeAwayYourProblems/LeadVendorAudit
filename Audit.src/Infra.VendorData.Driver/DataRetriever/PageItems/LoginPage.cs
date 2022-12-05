using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class LoginPage
{
   public LoginPage(ILoginPageInfo info)
   {
      User = info.User;
      Pass = info.Pass;
      ContinueButton = info.ContinueButton;
      Url = info.Url;
   }
   public string Url { get; }
   public By User { get; }
   public By Pass { get; }
   public By ContinueButton { get; }
}
