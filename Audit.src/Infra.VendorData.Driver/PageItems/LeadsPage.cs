using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class LeadsPage : ILeadsPage
{
   public LeadsPage(ILeadsPageInfo info)
   {
      LeadsElements = info.LeadsElement;
      NextPageButton = info.NextPageButton;
      NextPageUrl = info.NextPageUrl;
      FirstPageOfLeads = info.FirstPageOfLeads;
   }

   public By? LeadsElements { get; }
   public By? NextPageButton { get; }
   public string? FirstPageOfLeads { get; }
   public string? NextPageUrl { get; }
}