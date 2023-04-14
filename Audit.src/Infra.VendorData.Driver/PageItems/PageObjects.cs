namespace Infrastructure.VendorData.Driver;
public class PageObjects : IPageItems
{
   public PageObjects(ILoginPageInfo login, ILeadsPageInfo leads)
   {
      LoginPage = new LoginPage(login);
      LeadsPage = new LeadsPage(leads);
   }
   public ILoginPage LoginPage { get; }
   public ILeadsPage LeadsPage { get; }
}
