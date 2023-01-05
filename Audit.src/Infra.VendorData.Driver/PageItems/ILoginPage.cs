using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver
{
   public interface ILoginPage
   {
      public string Url { get; }
      public By User { get; }
      public By Pass { get; }
      public By ContinueButton { get; }
   }
}