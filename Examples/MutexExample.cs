namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.Mutex"/> class.
    /// </summary>
    public partial class MutexExample
    {
        #region Fields

        private Mutex mutex;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LockExample"/> class.
        /// </summary>
        public MutexExample()
        {
            this.mutex = new Mutex();
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="MutexExample"/>.
    /// </content>
    public partial class MutexExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.Mutex"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        var name = String.Format("Task {0}", data.taskNumber);

                        // Wait until it is safe to enter.
                        Console.WriteLine(String.Format("{0} is requesting the mutex", name));
                        this.mutex.WaitOne();

                        Console.WriteLine(String.Format("{0} has entered the protected area", name));

                        // Place code to access non-reentrant resources here - simulate work with Thread.Sleep
                        Thread.Sleep(500);
                        Console.WriteLine(String.Format("{0} is leaving the protected area", name));

                        // Release the Mutex.
                        this.mutex.ReleaseMutex();
                        Console.WriteLine(String.Format("{0} has released the mutex", name));
                    },
                    new { taskNumber = i });
                }

                Console.WriteLine("Tasks have been created, main thread complete, but background threads are still processing...");
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}

