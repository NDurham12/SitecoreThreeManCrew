namespace ContentByMail.Pipelines
{
    public interface IEmailMessageProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Process(PostmarkMessageArgs args);
    }
}
