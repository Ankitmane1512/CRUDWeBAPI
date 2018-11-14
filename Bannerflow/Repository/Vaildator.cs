using HtmlAgilityPack;
using System.Linq;
using System.Text;


namespace Bannerflow.Repository
{
    public class Vaildator
    {
        HtmlDocument doc;

        public string HtmlValidationMessage(string htmlToValidate)
        {
            StringBuilder sbIssues = new StringBuilder();
            int counter = 1;
            bool IsHtmlvalidated = false; 

            //Html is validated only if it contains some string.
            if (!string.IsNullOrEmpty(htmlToValidate)) IsHtmlvalidated = HtmlValidator(htmlToValidate);

            if (!IsHtmlvalidated)
            {
                sbIssues.Append("<Issues>");
                //HTML id invalid.It may be because of Empty string or thier are errors in document 
                //If string is null or empty than this block is executed else error with line number will be  returned
                if (string.IsNullOrEmpty(htmlToValidate)) sbIssues.Append("Null Or Empty HTML");
                else
                {
                    foreach (var htmlParseError in doc.ParseErrors)
                    {
                        //String Formatted to show errors
                        sbIssues.Append("<Issue> " + counter + " ");
                        sbIssues = sbIssues.Append("Line : " + htmlParseError.Line.ToString() + ", ");
                        sbIssues = sbIssues.Append("LinePosition : " + htmlParseError.LinePosition.ToString() + ", ");
                        sbIssues = sbIssues.Append("Reason : " + "'" + htmlParseError.Reason.ToString() + "', ");
                        sbIssues = sbIssues.Append("SourceText : " + "'" + htmlParseError.SourceText.ToString() + "' ");
                        sbIssues.Append("</Issue>");
                        counter++;
                    }
                }
               
                sbIssues.Append("</Issues>");
            }
            
            return sbIssues.ToString();
        }

        public bool HtmlValidator(string htmlToValidate)
        {
            doc = new HtmlDocument();
            //Html Agility pack used to load documentsa and check Inproper formed tags 
            doc.LoadHtml(htmlToValidate);
            if (doc.ParseErrors.Count() > 0) return false;
            return true;
        }
    }
}
