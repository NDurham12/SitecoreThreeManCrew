namespace ContentByMail.Pipelines.ItemWebApiRequest
{
    using Newtonsoft.Json;
    using PostmarkDotNet;
    using Sitecore.ItemWebApi.Pipelines.Request;
    using Sitecore.Pipelines;
    using System;
    using System.IO;
    using System.Web;

    public class PostmarkRequestProcessor : RequestProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Process(RequestArgs arguments)
        {
            HttpContext context = HttpContext.Current;

            if (context == null || context.Request == null || context.Request.InputStream == null)
                return;

            context.Request.InputStream.Position = 0;
            string json = null;

            using (StreamReader inputStream = new StreamReader(context.Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }

            if (!String.IsNullOrEmpty(json))
            {
                PostmarkInboundMessage message = JsonConvert.DeserializeObject<PostmarkInboundMessage>(json);

                if (message != null)
                {
                    CorePipeline.Run("ContentByMail.ProcessEmail", new PostmarkMessageArgs(message));
                }
            }
        }
    }
}