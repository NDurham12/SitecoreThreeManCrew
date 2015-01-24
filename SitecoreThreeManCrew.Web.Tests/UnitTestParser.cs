using System;
using System.Linq;
using ContentByMail.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PostmarkDotNet;

namespace SitecoreThreeManCrew.Web.Tests
{
    [TestClass]
    public class UnitTestParser
    {
        [TestMethod]
        public void EmailParserTestMethod()
        {
            var json = "{\r\n  \"From\": \"myUser@theirDomain.com\",\r\n  \"FromName\": \"My User\",\r\n  \"FromFull\": {\r\n    \"Email\": \"myUser@theirDomain.com\",\r\n    \"Name\": \"John Doe\",\r\n    \"MailboxHash\": \"\"\r\n  },\r\n  \"To\": \"451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com\",\r\n  \"ToFull\": [\r\n    {\r\n      \"Email\": \"451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com\",\r\n      \"Name\": \"\",\r\n      \"MailboxHash\": \"ahoy\"\r\n    }\r\n  ],\r\n  \"Cc\": \"\\\"Full name\\\" <sample.cc@emailDomain.com>, \\\"Another Cc\\\" <another.cc@emailDomain.com>\",\r\n  \"CcFull\": [\r\n    {\r\n      \"Email\": \"sample.cc@emailDomain.com\",\r\n      \"Name\": \"Full name\",\r\n      \"MailboxHash\": \"\"\r\n    },\r\n    {\r\n      \"Email\": \"another.cc@emailDomain.com\",\r\n      \"Name\": \"Another Cc\",\r\n      \"MailboxHash\": \"\"\r\n    }\r\n  ],\r\n  \"Bcc\": \"\\\"Full name\\\" <451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com>\",\r\n  \"BccFull\": [\r\n    {\r\n      \"Email\": \"451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com\",\r\n      \"Name\": \"Full name\",\r\n      \"MailboxHash\": \"ahoy\"\r\n    }\r\n  ],\r\n  \"OriginalRecipient\": \"451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com\",\r\n  \"ReplyTo\": \"myUsersReplyAddress@theirDomain.com\",\r\n  \"Subject\": \"This is an inbound message\",\r\n  \"MessageID\": \"22c74902-a0c1-4511-804f2-341342852c90\",\r\n  \"Date\": \"Thu, 5 Apr 2012 16:59:01 +0200\",\r\n  \"MailboxHash\": \"ahoy\",\r\n  \"TextBody\": \"[ASCII]\",\r\n  \"HtmlBody\": \"[Token This is my title]\r\n                        [Start]This is my data\r\n                        and this is more informaiton that i would like o add\r\n                        [/Start]\r\n                        [TokenB]dfddfdfdfd[/TokenB]\r\n                        [Start]This is my data\r\n                        and this is more informaiton that i would like o add\r\n                        [Start]This is my data\r\n                        and this is more informaiton that i would like o add\r\n                        [/Start]\",\r\n  \"StrippedTextReply\": \"Ok, thanks for letting me know!\",\r\n  \"Tag\": \"\",\r\n  \"Headers\": [\r\n    {\r\n      \"Name\": \"X-Spam-Checker-Version\",\r\n      \"Value\": \"SpamAssassin 3.3.1 (2010-03-16) onrs-ord-pm-inbound1.wildbit.com\"\r\n    },\r\n    {\r\n      \"Name\": \"X-Spam-Status\",\r\n      \"Value\": \"No\"\r\n    },\r\n    {\r\n      \"Name\": \"X-Spam-Score\",\r\n      \"Value\": \"-0.1\"\r\n    },\r\n    {\r\n      \"Name\": \"X-Spam-Tests\",\r\n      \"Value\": \"DKIM_SIGNED,DKIM_VALID,DKIM_VALID_AU,SPF_PASS\"\r\n    },\r\n    {\r\n      \"Name\": \"Received-SPF\",\r\n      \"Value\": \"Pass (sender SPF authorized) identity=mailfrom; client-ip=209.85.160.180; helo=mail-gy0-f180.google.com; envelope-from=myUser@theirDomain.com; receiver=451d9b70cf9364d23ff6f9d51d870251569e+ahoy@inbound.postmarkapp.com\"\r\n    },\r\n    {\r\n      \"Name\": \"DKIM-Signature\",\r\n      \"Value\": \"v=1; a=rsa-sha256; c=relaxed/relaxed;        d=wildbit.com; s=google;        h=mime-version:reply-to:date:message-id:subject:from:to:cc         :content-type;        bh=cYr/+oQiklaYbBJOQU3CdAnyhCTuvemrU36WT7cPNt0=;        b=QsegXXbTbC4CMirl7A3VjDHyXbEsbCUTPL5vEHa7hNkkUTxXOK+dQA0JwgBHq5C+1u         iuAJMz+SNBoTqEDqte2ckDvG2SeFR+Edip10p80TFGLp5RucaYvkwJTyuwsA7xd78NKT         Q9ou6L1hgy/MbKChnp2kxHOtYNOrrszY3JfQM=\"\r\n    },\r\n    {\r\n      \"Name\": \"MIME-Version\",\r\n      \"Value\": \"1.0\"\r\n    },\r\n    {\r\n      \"Name\": \"Message-ID\",\r\n      \"Value\": \"<CAGXpo2WKfxHWZ5UFYCR3H_J9SNMG+5AXUovfEFL6DjWBJSyZaA@mail.gmail.com>\"\r\n    }\r\n  ],\r\n  \"Attachments\": [\r\n    {\r\n      \"Name\": \"myimage.png\",\r\n      \"Content\": \"[BASE64-ENCODED CONTENT]\",\r\n      \"ContentType\": \"image/png\",\r\n      \"ContentLength\": 4096,\r\n      \"ContentID\": \"myimage.png@01CE7342.75E71F80\"\r\n    },\r\n    {\r\n      \"Name\": \"mypaper.doc\",\r\n      \"Content\": \"[BASE64-ENCODED CONTENT]\",\r\n      \"ContentType\": \"application/msword\",\r\n      \"ContentLength\": 16384,\r\n      \"ContentID\": \"\"\r\n    }\r\n  ]\r\n}";

            PostmarkInboundMessage message = JsonConvert.DeserializeObject<PostmarkInboundMessage>(json);

            Assert.IsNotNull(message, "Unable to DeserializeObject");
            
            var output = EmailParser.ParseTokens(message);

            Assert.AreEqual(output.Count, 3, "Output didn't produce 3 matches");

            Assert.IsFalse(output.Any(m => m.Value.Contains("[")), "Token [ character found");

            Assert.IsFalse(output.Any(m => m.Value.Contains("]")), "Token ] character found");

        }
    }
}
