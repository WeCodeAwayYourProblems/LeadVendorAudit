namespace Infrastructure.VendorData.Driver;
public class PageItems
{
   public PageItems(GetLoginPageItemsFromCsv login, GetLeadsPageItemsFromCsv leads)
   {
      LoginPage = new(login);
      LeadsPage = new(leads);
   }
   public LoginPage LoginPage { get; }
   public LeadsPage LeadsPage { get; }
}
