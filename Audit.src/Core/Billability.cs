namespace CoreLogic;

public class Billability
{
   public required bool IsBillable { get; set; }
   private string? billabilityReasoning;
   public string? BillabilityReasoning
   {
      get { return billabilityReasoning; }
      set
      {
         if (IsBillable)
            billabilityReasoning = default;
         else
         { billabilityReasoning = value; }
      }
   }
}