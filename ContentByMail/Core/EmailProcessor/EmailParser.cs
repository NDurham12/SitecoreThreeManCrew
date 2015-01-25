using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ContentByMail.Common;
using PostmarkDotNet;
using Sitecore.Diagnostics;
using Sitecore.Extensions.StringExtensions;

namespace ContentByMail.Core.EmailProcessor
{
    internal class EmailParser
    {
        internal static Dictionary<string, string> ParseTokens(PostmarkInboundMessage inboundMessage)
        {
            var listOfTokens = new Dictionary<string, string>();

            try
            {
                var fullRegex = String.Format("{0}|{1}|{2}", Constants.Settings.TokenStartEndMultilineRegex,
                    Constants.Settings.TokenTextInside, Constants.Settings.TokenMissingEnding);

                var matches = Regex.Matches(inboundMessage.HtmlBody + "\n" + inboundMessage.TextBody, fullRegex,
                    RegexOptions.Multiline);

                for (var index = 0; index < matches.Count; index++)
                {
                    try
                    {
                        var text = matches[index].Value;
                        var endofToken = Int32.MaxValue;

                        if (text.IndexOf(" ", 1) > 0) endofToken = text.IndexOf(" ", 1);
                        if (text.IndexOf("[", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("[", 1));
                        if (text.IndexOf("]", 1) > 0) endofToken = Math.Min(endofToken, text.IndexOf("]", 1));

                        endofToken -= 1; //go back 1 character

                        endofToken = endofToken < 0 ? 0 : endofToken; //Ensure end is not past string length

                        var token = text.Mid(1, endofToken);
                        var value =
                            text.Replace("[" + token + "]", "")
                                .Replace("[/" + token + "]", "")
                                .Replace("[" + token, "")
                                .Replace("]", "")
                                .Trim(); //get rid of token characters

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
                        Log.Error("ParseTokens - Token Loop", ex, typeof (EmailParser));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ParseTokens", ex, typeof (EmailParser));
            }

            return listOfTokens;
        }
    }
}