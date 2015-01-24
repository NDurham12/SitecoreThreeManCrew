using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PostmarkDotNet;
using Sitecore.Diagnostics;
using Sitecore.Extensions.StringExtensions;
using Sitecore.Search;

namespace ContentByMail.Common
{
    public class EmailParser
    {
        public static Dictionary<string, string> ParseTokens(PostmarkInboundMessage inboundMessage)
        {
            var listOfTokens = new Dictionary<string, string>();

            try
            {
                var fullRegex = string.Format("{0}|{1}|{2}", Constants.Settings.TokenStartEndMultilineRegex, Constants.Settings.TokenTextInside, Constants.Settings.TokenMissingEnding);

                var matches = Regex.Matches(inboundMessage.HtmlBody + "\n" + inboundMessage.TextBody, fullRegex, RegexOptions.Multiline);            

                for (var iIndex = 0; iIndex < matches.Count; iIndex++)
                {
                    try
                    {
                        var text = matches[iIndex].Value;
                        var endofToken = int.MaxValue;

                        if (text.IndexOf(" ", 1) > 0) endofToken = text.IndexOf(" ", 1);
                        if (text.IndexOf("[", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("[", 1));
                        if (text.IndexOf("]", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("]", 1));
                        endofToken -= 1; //go back 1 character
                                
                        endofToken = endofToken < 0 ? 0 : endofToken; //Ensure end is not past string length

                        var token = text.Mid(1, endofToken);
                        var value = text.Replace("[" + token + "]", "").Replace("[/" + token + "]", "").Replace("[" + token, "").Replace("]", "").Trim(); //get rid of token characters
                
                        if (listOfTokens.ContainsKey(token))
                            listOfTokens[token] += value;
                        else 
                            listOfTokens.Add(token,value);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("ParseTokens - Token Loop", ex, typeof(EmailParser));
                    }
                }                
            }
            catch (Exception ex)
            {
                Log.Error("ParseTokens", ex, typeof(EmailParser));
            }

            return listOfTokens;
        }        

    }
}
