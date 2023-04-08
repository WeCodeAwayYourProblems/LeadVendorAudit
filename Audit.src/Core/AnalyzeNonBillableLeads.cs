namespace CoreLogic;

public class AnalyzeNonBillableLeads
{
   public IEnumerable<ILead> QualifyAll(IEnumerable<ILead> leads, IEnumerable<ILeadAnalysisParameters> parameters)
   {
      foreach (var lead in leads)
         foreach (var p in parameters)
            if (lead.Billability is null && p.IsMatch(lead.Notes!))
               lead.Billability = new Billability() { IsBillable = false, BillabilityReasoning = p.Reasoning };

      return leads;
   }
}