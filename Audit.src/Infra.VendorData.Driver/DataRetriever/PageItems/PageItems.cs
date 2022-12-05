using System;

namespace Infrastructure.VendorData.Driver;
public class PageItems
{
   public PageItems()
   {
      LoginPage = new(new GetLoginPageItemsFromCsv(Home + RelativePath + ".info/LoginPageInfo.csv"));
      LeadsPage = new(new GetLeadsPageItemsFromCsv(Home + RelativePath + ".info/LeadsPageElements.csv"));
   }
   private readonly string Home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
   private readonly string RelativePath = "/CS_area/LeadVendorAudit/LeadVendorAudit.src/Infrastructure/VendorData/VendorDataRetrieval/PageItems/";
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
