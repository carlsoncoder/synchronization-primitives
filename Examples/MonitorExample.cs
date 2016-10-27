namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.Monitor"/> class.
    /// </summary>
    public partial class MonitorExample
    {
        #region Fields

        /// <summary>
        /// The object that we'll lock onto with our Monitor object.
        /// </summary>
        private static object SyncLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorExample"/> class.
        /// </summary>
        public MonitorExample()
        {
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="MonitorExample"/>.
    /// </content>
    public partial class MonitorExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.Monitor"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            int numberOfTasks = 0;
            List<Task> tasks = new List<Task>();

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(250);
                        Monitor.Enter(MonitorExample.SyncLock);
                        try
                        {
                            numberOfTasks++;
                        }
                        finally
                        {
                            Monitor.Exit(MonitorExample.SyncLock);
                        }
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine(String.Format("{0} tasks started and executed", numberOfTasks));
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}
