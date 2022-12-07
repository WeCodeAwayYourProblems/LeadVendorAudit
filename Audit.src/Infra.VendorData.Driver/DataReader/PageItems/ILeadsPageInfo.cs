using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public interface ILeadsPageInfo
{
   public By? LeadsElement { get; set; }
   public By? NextPageButton { get; set; }
   public string? NextPageUrl { get; set; }
}