namespace AuditCore;
public record PhoneNumber(string number)
{
   private int num;
   public int Number
   {
      get { return num; }
      set
      {
         bool converted = int.TryParse(number, out int conversion);
         if (number.Length == 10 && converted)
            num = conversion;
         else
            throw new ArgumentException($"Parameter {nameof(number)} must be a string with no more and no less than ten (10) digits.");
      }
   }

}