using CliHelperClass;
using OpenQA.Selenium;

namespace Infrastructure.VendorData.Driver;
public class LoginPageInfoFromCsv : ILoginPageInfo
{
   public By User { get; set; }
   public By Pass { get; set; }
   public By ContinueButton { get; set; }
   public string Url { get; set; }
   public required WebDriverManipulator WebD { get; init; }
   public LoginPageInfoFromCsv(string pathToCsv)
   {
      string[] lines = File.ReadAllLines(pathToCsv);
      int userIndex = default;
      int passIndex = default;
      int buttonIndex = default;
      string url = "";
      for (var line = 0; line < lines.Length; line++)
      {
         switch (lines[line].Split(",")[0].ToLower())
         {
            case "user":
               userIndex = line;
               break;
            case "pass":
               passIndex = line;
               break;
            case "button":
               buttonIndex = line;
               break;
            case "url":
               url = lines[line].Split(',')[2];
               break;
            default:
               throw new Exception($"There is a problem with the {nameof(LoginPage)} page element file");
         }
      }
      User = SetBy(lines[userIndex].Split(','));
      Pass = SetBy(lines[passIndex].Split(','));
      ContinueButton = SetBy(lines[buttonIndex].Split(','));
      Url = url;
   }

   public By SetBy(string[] line) =>
   WebD.StringToBy(line[1], line[2]);
}