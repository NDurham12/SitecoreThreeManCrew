using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using PostmarkDotNet;
using Sitecore.Diagnostics;
using Sitecore.ItemWebApi.Pipelines.Request;
using Sitecore.Pipelines;
using ThreeManCrew.ContentByMail.Common;
using ThreeManCrew.ContentByMail.Core.ContentEmailManager;
using ThreeManCrew.ContentByMail.Core.Notifications;
using ThreeManCrew.ContentByMail.Core.RequestHistory;

namespace ThreeManCrew.ContentByMail.Pipelines.ItemWebApiRequest
{
    public class PostmarkRequestProcessor : RequestProcessor
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Process(RequestArgs arguments)
        {
            var context = HttpContext.Current;

            if (context == null || context.Request == null || context.Request.InputStream == null)
                return;

            context.Request.InputStream.Position = 0;
            string json = null;

            try
            {
                using (var inputStream = new StreamReader(context.Request.InputStream))
                {
                    json = inputStream.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(json))
                {
                    var message = JsonConvert.DeserializeObject<PostmarkInboundMessage>(json);

                    if (message != null)
                    {
                        CorePipeline.Run("ContentByMail.ProcessEmail", new PostmarkMessageArgs(message));
                        EmailRequestHistory.Add(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Cannot process Postmark request.", ex, this);
                NotificationManager.Send(ContentEmailManagerTemplate.FallBackAddress, ContentEmailManagerTemplate.DefaultMessage, NotificationMessageType.Failure);
            }
        }
    }
}