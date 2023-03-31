namespace Infrastructure.VendorData.Driver;
public class PageObjects : IPageItems
{
   public PageObjects(ILoginPageInfo login, ILeadsPageInfo leads)
   {
      LoginPage = new(login);
      LeadsPage = new(leads);
   }
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
