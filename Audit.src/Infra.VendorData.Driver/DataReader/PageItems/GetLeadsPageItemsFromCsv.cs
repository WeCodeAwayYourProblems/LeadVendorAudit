using CliHelperClass;
using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class GetLeadsPageItemsFromCsv : WebDriverManipulator, ILeadsPageInfo
{
   public GetLeadsPageItemsFromCsv(string path)
   {
      string[] lines = File.ReadAllLines(path);
      int leadsElem = default;
      int nextPageBtn = default;
      string nextPgUrl = "";
      for (var line = 0; line < lines.Length; line++)
      {
         switch (lines[line].Split(",")[0].ToLower())
         {
            case "leads":
               leadsElem = line;
               break;
            case "button":
               nextPageBtn = line;
               break;
            case "url":
               nextPgUrl = lines[line].Split(',')[2];
               break;
            default:
               throw new Exception($"There is a problem with the {nameof(LeadsPage)} page elements file.");
         }
      }
      LeadsElement = SetBy(lines[leadsElem].Split(","));
      NextPageButton = SetBy(lines[nextPageBtn].Split(","));
      NextPageUrl = nextPgUrl;
   }

   public By SetBy(string[] line) =>
   StringToBy(line[1], line[2]);

   public By LeadsElement { get; set; }
   public By NextPageButton { get; set; }
   public string NextPageUrl { get; set; }
}