namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the 'lock' keyword.
    /// </summary>
    public partial class LockExample
    {
        #region Fields

        /// <summary>
        /// The object that we'll actually lock on when updating our values.
        /// </summary>
        private static object SyncLock = new object();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LockExample"/> class.
        /// </summary>
        public LockExample()
        {
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="LockExample"/>.
    /// </content>
    public partial class LockExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the lock keyword.
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
                        lock (LockExample.SyncLock)
                        {
                            numberOfTasks++;
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
