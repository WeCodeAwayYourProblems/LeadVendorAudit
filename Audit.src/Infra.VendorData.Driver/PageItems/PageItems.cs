namespace Infrastructure.VendorData.Driver;
public class PageItems : IPageItems
{
   public PageItems(ILoginPageInfo login, ILeadsPageInfo leads)
   {
      LoginPage = new(login);
      LeadsPage = new(leads);
   }
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
