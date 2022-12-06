using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class LeadsPage
{
   public LeadsPage(ILeadsPageInfo info)
   {
      LeadsElements = info.LeadsElement;
      NextPageButton = info.NextPageButton;
      NextPageUrl = info.NextPageUrl;
   }

   public By LeadsElements { get; }
   public By NextPageButton { get; }
   public string NextPageUrl { get; }
}