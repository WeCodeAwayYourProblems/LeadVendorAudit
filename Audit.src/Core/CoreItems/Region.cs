namespace AuditCore;
public class Region
{
   public Region(string state, string city, string zip)
   {
      State = state;
      City = city;
      int conversion;
      if (zip.Length != 5 || !int.TryParse(zip, out conversion))
         throw new ArgumentException($"Parameter {nameof(zip)} must be a 5-digit zip code without any additional characters.");
      else
      {
         ZipCode = conversion;
         ZipString = zip;
      }
   }
   public string State { get; }
   public string City { get; }
   public string ZipString { get; }
   public int ZipCode { get; }

}