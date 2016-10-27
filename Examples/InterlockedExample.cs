namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.Interlocked"/> class.
    /// </summary>
    public partial class InterlockedExample
    {
        #region Fields

        private static int Counter = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedExample"/> class.
        /// </summary>
        public InterlockedExample()
        {
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="InterlockedExample"/>.
    /// </content>
    public partial class InterlockedExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.Interlocked"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            List<Task> tasks = new List<Task>();

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Thread.Sleep(250);
                        Interlocked.Increment(ref InterlockedExample.Counter);
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine(String.Format("Counter is at {0}, should be at 10", InterlockedExample.Counter));
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}
