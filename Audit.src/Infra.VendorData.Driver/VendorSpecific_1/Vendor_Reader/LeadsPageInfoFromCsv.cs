using CliHelperClass;
using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class LeadsPageInfoFromCsv : ILeadsPageInfo
{
   public By LeadsElement { get; set; }
   public By NextPageButton { get; set; }
   public string NextPageUrl { get; set; }
   public string FirstPageOfLeads { get; }
   public required WebDriverManipulator WebD { get; init; }
   public LeadsPageInfoFromCsv(string pathToCsv)
   {
      // CSV reading
      string[] lines = File.ReadAllLines(pathToCsv);
      int leadsElem = default;
      int nextPageBtn = default;
      string nextPgUrl = "";
      string firstPageOfLeads = "";
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
            case "leadspage":
               firstPageOfLeads = lines[line].Split(',')[2];
               break;
            default:
               throw new Exception($"There is a problem with the {nameof(LeadsPage)} page elements file.");
         }
      }
      LeadsElement = SetBy(lines[leadsElem].Split(","));
      NextPageButton = SetBy(lines[nextPageBtn].Split(","));
      NextPageUrl = nextPgUrl;
      FirstPageOfLeads = firstPageOfLeads;
   }

   public By SetBy(string[] line) =>
      WebD.StringToBy(line[1], line[2]);
}