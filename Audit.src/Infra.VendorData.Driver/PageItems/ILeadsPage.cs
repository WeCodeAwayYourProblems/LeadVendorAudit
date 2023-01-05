using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver
{
   public interface ILeadsPage
   {
      public By? LeadsElements { get; }
      public By? NextPageButton { get; }
      public string? FirstPageOfLeads { get; }
      public string? NextPageUrl { get; }
   }
}