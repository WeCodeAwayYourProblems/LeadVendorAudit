using System.Text.RegularExpressions;

namespace CoreLogic;
public interface ILeadAnalysisParameters
{
   public Regex Pattern { get; set; }
   public string Reasoning { get; set; }
   public bool IsMatch(string leadNotes);
}