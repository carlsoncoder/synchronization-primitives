namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.Semaphore"/> class.
    /// </summary>
    public partial class SemaphoreExample
    {
        #region Fields

        private Semaphore semaphore;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SemaphoreExample"/> class.
        /// </summary>
        public SemaphoreExample()
        {
            this.semaphore = new Semaphore(3, 3);
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="SemaphoreExample"/>.
    /// </content>
    public partial class SemaphoreExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.Semaphore"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            try
            {
                for (int i = 0; i < 15; i++)
                {
                    Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        var name = String.Format("Task {0}", data.taskNumber);

                        // Wait to enter the the semaphore (wait for someone to release from the semaphore)
                        Console.WriteLine(String.Format("{0} is waiting to enter the semaphore...", name));
                        this.semaphore.WaitOne();

                        // simulate doing some work
                        Console.WriteLine(String.Format("{0} is doing some work...", name));
                        Thread.Sleep(500);

                        // Done working, release the semaphore
                        Console.WriteLine(String.Format("{0} is releasing the semaphore...", name));
                        this.semaphore.Release(1);
                    },
                    new { taskNumber = i });
                }

                Console.WriteLine("Semaphore Example has completed!");
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}

