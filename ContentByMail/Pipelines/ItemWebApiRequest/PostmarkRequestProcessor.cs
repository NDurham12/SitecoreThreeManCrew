namespace ContentByMail.Pipelines.ItemWebApiRequest
{
    using PostmarkDotNet;
    using Sitecore.Diagnostics;
    using Sitecore.ItemWebApi.Pipelines.Request;
    using Sitecore.Pipelines;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Script.Serialization;

    class PostmarkRequestProcessor : RequestProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public override void Process(RequestArgs arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            HttpContext context = HttpContext.Current;

            if (context == null || context.Request == null || context.Request.InputStream == null)
                return;

            context.Request.InputStream.Position = 0;
            string json = null;

            using (var inputStream = new StreamReader(context.Request.InputStream))
            {
                json = inputStream.ReadToEnd();
            }

            if (!String.IsNullOrEmpty(json))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                PostmarkMessage message = javaScriptSerializer.Deserialize(json, typeof(PostmarkMessage)) as PostmarkMessage;

                if (message != null)
                {
                    CorePipeline.Run("Hackathon.ProcessEmail", new PostmarkMessageArgs(message));
                }
            }
        }
    }
}