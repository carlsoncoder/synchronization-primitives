namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.CountdownEvent"/> class.
    /// </summary>
    public partial class CountdownEventExample
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CountdownEventExample"/> class.
        /// </summary>
        public CountdownEventExample()
        {
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="CountdownEventExample"/>.
    /// </content>
    public partial class CountdownEventExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.CountdownEvent"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            try
            {
                // initialize countdown count to 1 right away to prevent the object 
                // from being immediately signalled...we decrement outside of the loop to remove it (see below)
                using (CountdownEvent countdown = new CountdownEvent(1))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        // increment the counter signifying we have another task to work on
                        countdown.AddCount();

                        // execute the task/thread
                        Task.Factory.StartNew((Object obj) =>
                        {
                            var data = (dynamic)obj;
                            var name = String.Format("Task {0}", data.taskNumber);
                            Thread.Sleep(TimeSpan.FromSeconds(3));
                            Console.WriteLine(String.Format("{0} Completed", name));

                            // signal to the CountdownEvent that this task is done
                            countdown.Signal();
                        },
                        new { taskNumber = i });
                    }

                    // decrement the first one we put in place
                    countdown.Signal();

                    // wait for all to complete
                    countdown.Wait();
                    Console.WriteLine("All events in CountdownEvent have completed!");
                }
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}
