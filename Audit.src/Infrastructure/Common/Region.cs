using CoreLogic;

namespace AuditInfrastructure;
public class Region : IRegion
{
   // ctors: Zip as integer
   public Region(string country, string state, string city, int zip, string marketName) : this(state, city, zip, marketName)
   { Country = country; }
   public Region(string state, string city, int zip, string marketName) : this(city, zip, marketName)
   { State = state; }
   public Region(string city, int zip, string marketName) : this(zip, marketName)
   { City = city; }
   public Region(int zip, string marketName) : this(marketName)
      => ValidateIntZipInput(zip);

   // ctors: Zip as string
   public Region(string country, string state, string city, string zip, string marketName) : this(state, city, zip, marketName)
   { Country = country; }
   public Region(string state, string city, string zip, string marketName) : this(city, zip, marketName)
   { State = state; }
   public Region(string city, string zip, string marketName) : this(zip, marketName)
   { City = city; }
   public Region(string zip, string marketName) : this(marketName)
      => ConvertZipStringToInt(zip);
   public Region(string marketName)
   { MarketName = marketName; }

   // Public attributes
   public string? Country { get; set; }
   public string? State { get; set; }
   public string? City { get; set; }
   public int? ZipCode { get; set; }
   public string MarketName { get; set; }

   // Private attributes -- zip code validation and converters
   private void ValidateIntZipInput(int zip)
      => ConvertZipStringToInt($"{zip}");
   private void ConvertZipStringToInt(string zip)
   {
      int conversion;
      if (zip.Length != 5 || !int.TryParse(zip, out conversion))
         throw new ArgumentException($"Parameter {nameof(zip)} must be a 5-digit zip code without any additional characters.");
      else
      { ZipCode = conversion; }
   }
}