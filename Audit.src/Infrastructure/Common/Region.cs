using AuditCore;

namespace AuditInfrastructure;
public class Region : IRegion
{
   // Constructors
   // Zip as integer ctors
   public Region(string country, string state, string city, int zip) : this(state, city, zip)
   { Country = country; }
   public Region(string state, string city, int zip) : this(city, zip)
   { State = state; }
   public Region(string city, int zip) : this(zip)
   { City = city; }
   public Region(int zip)
      => ValidateIntZipInput(zip);

   // Zip as string ctors
   public Region(string country, string state, string city, string zip) : this(state, city, zip)
   { Country = country; }
   public Region(string state, string city, string zip) : this(city, zip)
   { State = state; }
   public Region(string city, string zip) : this(zip)
   { City = city; }
   public Region(string zip)
      => ConvertZipStringToInt(zip);

   // Public attributes
   public string? Country { get; set; }
   public string? State { get; set; }
   public string? ZipString { get; set; }
   public string? City { get; set; }
   public int ZipCode { get; set; }

   // Private attributes -- zip code validation and converters
   private void ValidateIntZipInput(int zip)
      => ConvertZipStringToInt($"{zip}");
   private void ConvertZipStringToInt(string zip)
   {
      int conversion;
      if (zip.Length != 5 || !int.TryParse(zip, out conversion))
         throw new ArgumentException($"Parameter {nameof(zip)} must be a 5-digit zip code without any additional characters.");
      else
      {
         ZipCode = conversion;
         ZipString = zip;
      }
   }
}