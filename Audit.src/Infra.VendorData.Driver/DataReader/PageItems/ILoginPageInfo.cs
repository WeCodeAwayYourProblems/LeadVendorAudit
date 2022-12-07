using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;

public interface ILoginPageInfo
{
   public By User { get; set; }
   public By Pass { get; set; }
   public By ContinueButton { get; set; }
   public string Url { get; set; }
}