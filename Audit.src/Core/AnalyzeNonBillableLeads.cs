namespace CoreLogic;

public class AnalyzeNonBillableLeads
{
   public AnalyzeNonBillableLeads(string errLogPath_Absolute_csv)
   {
      ErrLogPath = errLogPath_Absolute_csv;
   }
   public IEnumerable<ILead> QualifyAll(IVendorRecord vendor, IEnumerable<ILead> leads, IEnumerable<ILeadAnalysisParameters> parameters, out ILead[] unqualified)
   {
      foreach (var lead in leads)
         foreach (var p in parameters)
            if (lead.Billability is null && p.IsMatch(lead.Notes!))
               lead.Billability = new Billability() { IsBillable = false, BillabilityReasoning = p.Reasoning };

      // Check for failures and log them as errors
      unqualified = leads.Where(lead => lead.Billability is null).ToArray();
      if (unqualified.Length > 0)
         NonBillableLeadsAnalyzer_ErrLog(unqualified, vendor);

      return leads;
   }

   public void NonBillableLeadsAnalyzer_ErrLog(IEnumerable<ILead> leads, IVendorRecord vendor)
   {
      string errMsg = $"The following lead notes could not be qualified by the parameters set by the following vendor ##Update required##";
      foreach (var lead in leads)
      {
         string notes = $"Notes: {lead.Notes!}, ";
         string text = errMsg + "," + $"Vendor: {vendor.VendorName}" + "," + notes + "\n";
         File.AppendAllText(ErrLogPath, text);
      }
   }
   private string ErrLogPath { get; set; }
}