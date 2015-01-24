namespace ContentByMail.Pipelines.ItemWebApiRequest
{
    using Newtonsoft.Json;
    using PostmarkDotNet;
    using Sitecore.Diagnostics;
    using Sitecore.ItemWebApi.Pipelines.Request;
    using Sitecore.Pipelines;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Script.Serialization;

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
                object message = null;
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                try
                {
                    //message = serializer.Deserialize(json, typeof(PostmarkMessage));
                    message = JsonConvert.DeserializeObject<PostmarkMessage>(json);
                }
                catch(Exception ex)
                {
                    Log.Error("", ex, this);
                }

                if (message != null && message is PostmarkMessage)
                {
                    CorePipeline.Run("Hackathon.ProcessEmail", new PostmarkMessageArgs(message as PostmarkMessage));
                }
            }
        }
    }
}