using AuditCore;

namespace AuditInfrastructure;
public class Region : IRegion
{
   public Region(string state, string city, string zip) : this(state, city)
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
   public Region(string state, string city) : this(city)
   {
      State = state;
   }
   public Region(string city)
   {
      City = city;
   }
   public string? State { get; set; }
   public string? ZipString { get; set; }
   public string City { get; set; }
   public int ZipCode { get; }

}