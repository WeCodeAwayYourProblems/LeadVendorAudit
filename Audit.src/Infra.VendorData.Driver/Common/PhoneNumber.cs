using AuditCore;
using System.Text.RegularExpressions;

namespace Infrastructure.VendorData.Driver;
public class PhoneNumber : IPhoneNumber
{
   public PhoneNumber(string number)
   {
      this.number = number;
   }
   private readonly string number;
   private int num;
   public int Number
   {
      get { return num; }
      set
      {
         bool converted = int.TryParse(number, out int conversion);
         bool regexConverted = int.TryParse
         (
            string.Join
            (
               null,
               new Regex(@"+1|\(|\)|\s|-").Split(number)
            ),
            out int conversion2
         );

         if (converted)
            num = conversion;
         else if (regexConverted)
            num = conversion2;
         else
            throw new ArgumentException($"The Parameter called {nameof(number)} must be a string that matches the following regex:\n@\"(+1)?\\(?\\d{3}(\\)|\\s|-)?\\d{3}(\\s|-)?\\d{4}\"");
      }
   }

}