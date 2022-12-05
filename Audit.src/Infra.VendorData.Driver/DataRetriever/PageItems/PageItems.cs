namespace Infrastructure.VendorData.Driver;
public class PageItems
{
   public PageItems()
   {
      LoginPage = new(new GetLoginPageItemsFromCsv(RelativePath + ".info/LoginPageInfo.csv"));
      LeadsPage = new(new GetLeadsPageItemsFromCsv(RelativePath + ".info/LeadsPageElements.csv"));
   }
   private readonly string RelativePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/CS_area/LeadVendorAudit/LeadVendorAudit.src/Infrastructure/VendorData/VendorDataRetrieval/PageItems/";
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
