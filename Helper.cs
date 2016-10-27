namespace SynchronizationPrimitives
{
    using System;

    /// <summary>
    /// Helper class for examples.
    /// </summary>
    public static class Helper
    {
        #region Methods

        /// <summary>
        /// Handles printing out details from a <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="ex">The <see cref="AggregateException"/> to handle.</param>
        public static void HandleAggregateException(AggregateException ex)
        {
            string fullMessage = String.Empty;
            foreach (var innerEx in ex.InnerExceptions)
            {
                Console.WriteLine(innerEx.GetType().Name);
                if (!fullMessage.Contains(innerEx.Message))
                {
                    fullMessage += innerEx.Message + Environment.NewLine;
                }
            }

            Console.WriteLine("Exception Message(s):");
            Console.WriteLine(fullMessage);
        }

        #endregion
    }
}
