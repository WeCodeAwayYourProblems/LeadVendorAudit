namespace Infrastructure.VendorData.Driver;
public class PageItems
{
   public PageItems(GetLoginPageItemsFromCsv login, GetLeadsPageItemsFromCsv leads)
   {
      LoginPage = new(login);
      LeadsPage = new(leads);
   }
   private readonly string RelativePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/CS_area/LeadVendorAudit/LeadVendorAudit.src/Infrastructure/VendorData/VendorDataRetrieval/PageItems/";
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
