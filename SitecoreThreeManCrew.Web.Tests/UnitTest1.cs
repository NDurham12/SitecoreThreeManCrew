using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Text;
using ContentByMail.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostmarkDotNet;
using PostmarkDotNet.Legacy;

namespace SitecoreThreeManCrew.Web.Tests
{
    [TestClass]
    public class UnitTestEmailTest
    {       
        [TestMethod]
        public void Test5()
        {            
            // Example request
            PostmarkMessage message = new PostmarkMessage {
                From = "ThreeManCrew@sitecoregirl.net",
                To = "mrsnonadurham@gmail.com",
                Subject = "Hello from Postmark",
                HtmlBody = "<strong>Hello</strong> dear Postmark user.",
                TextBody = "Hello dear postmark user.",
                ReplyTo = "reply@example.com",
                TrackOpens = true,
                Headers =  new PostmarkDotNet.Model.HeaderCollection ()
            };

            
            PostmarkClient client = new PostmarkClient("d7a81168-a3e5-43bc-bbba-14e9da6869bd");

            PostmarkResponse response = client.SendMessage(message);

            if(response.Status != PostmarkStatus.Success) {
                Console.WriteLine("Response was: " + response.Message);
            }
        }
    }
}
