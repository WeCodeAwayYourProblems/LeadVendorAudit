namespace Infrastructure.VendorData.Driver
{
   public interface IPageItems
   {
      public ILoginPage LoginPage { get; }
      public ILeadsPage LeadsPage { get; }
   }
}