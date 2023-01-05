using AuditCore;

namespace Infrastructure.VendorData.Driver;

public class WriteDataToVendor_Driver : LoginProtocol_RW, IVendorDataWriter
{
   public bool WriteDataToVendor(IEnumerable<ICallLead> notBillableLeads)
   {
      bool success = false;
      while (!success)
      {
         bool opened = OpenVendorWebsite(Page.LoginPage.Url);
         bool loggedIn = LogIntoVendorSite(Logins!);
         bool navigated = NavigateToAppropriatePage();
         bool entered = EnterFormulatedSalesData(StartDate, EndDate, notBillableLeads);
         if (!opened || !loggedIn || !navigated || !entered)
         {
            failureProtocol();
            continue;
         }
      }
      return true;
      void failureProtocol()
      {
         WebD.CloseChrome();
      }
   }

   public bool EnterFormulatedSalesData(DateTime startDate, DateTime endDate, IEnumerable<ICallLead> notBillableLeads)
   {
      foreach (var lead in notBillableLeads)
      {
         GoToFirstPagesOfLeadsList();
         FindLeadOnCurrentPage(lead);
         RequestCreditAsAppropriate(lead);
      }
      return true;
   }

   private void GoToFirstPagesOfLeadsList()
   {
      bool navigated = default;
      do
      {
         navigated = NavigatedToFirstPageOfLeads();
      } while (!navigated);
   }

   private void FindLeadOnCurrentPage(ICallLead lead)
   {
      int pageNum = 1;
      bool found = default;
      do
      {
         found = FindLeadOnPage(lead);
         if (!found)
         {
            NavigateToNextPage(pageNum);
            pageNum++;
         }
      }
      while (!found);
   }

   private void RequestCreditAsAppropriate(ICallLead lead)
   {
      bool requested = default;
      do
      {
         requested = RequestCredits(lead);
      } while (!requested);
   }

   public bool RequestCredits(ICallLead lead)
   {
      throw new NotImplementedException();
   }

   public bool NavigatedToFirstPageOfLeads()
   {
      WebD.Chrome!.Url = Page.LeadsPage.FirstPageOfLeads;
      return true;
   }

   public void NavigateToNextPage(int pageNumber)
   {
      bool navigated = default;
      do
      {
         navigated = SuccessfullyNavigatedToNextPage();
      } while (!navigated);
   }

   public bool SuccessfullyNavigatedToNextPage()
   {
      throw new NotImplementedException();
   }

   public bool FindLeadOnPage(ICallLead lead)
   {
      throw new NotImplementedException();
   }
}