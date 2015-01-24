namespace ContentByMail.Pipelines.ContentByMail.ProcessEmail
{
    using Sitecore.Diagnostics;

    public class SimpleEmailMessageProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleEmailMessageProcessor"/> class.
        /// </summary>
        public SimpleEmailMessageProcessor() { }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Process(PostmarkMessageArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.Message, "args.Message");

            Log.Info("In SimpleEmailMessageProcessor", this);
        }
    }
}
