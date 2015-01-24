using PostmarkDotNet;
using Sitecore.Diagnostics;
using Sitecore.Extensions.StringExtensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ContentByMail.Common
{
    public class EmailParser
    {
        public static Dictionary<string, string> ParseTokens(PostmarkInboundMessage inboundMessage)
        {
            Dictionary<string, string> listOfTokens = new Dictionary<string, string>();

            try
            {
                string fullRegex = String.Format("{0}|{1}|{2}", Constants.Settings.TokenStartEndMultilineRegex, Constants.Settings.TokenTextInside, Constants.Settings.TokenMissingEnding);

                MatchCollection matches = Regex.Matches(inboundMessage.HtmlBody + "\n" + inboundMessage.TextBody, fullRegex, RegexOptions.Multiline);            

                for (int index = 0; index < matches.Count; index++)
                {
                    try
                    {
                        string text = matches[index].Value;
                        int endofToken = Int32.MaxValue;

                        if (text.IndexOf(" ", 1) > 0) endofToken = text.IndexOf(" ", 1);
                        if (text.IndexOf("[", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("[", 1));
                        if (text.IndexOf("]", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("]", 1));
                        
                        endofToken -= 1; //go back 1 character
                                
                        endofToken = endofToken < 0 ? 0 : endofToken; //Ensure end is not past string length

                        string token = text.Mid(1, endofToken);
                        string value = text.Replace("[" + token + "]", "").Replace("[/" + token + "]", "").Replace("[" + token, "").Replace("]", "").Trim(); //get rid of token characters

                        if (listOfTokens.ContainsKey(token))
                        {
                            listOfTokens[token] += value;
                        }
                        else
                        {
                            listOfTokens.Add(token, value);
                        }
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
