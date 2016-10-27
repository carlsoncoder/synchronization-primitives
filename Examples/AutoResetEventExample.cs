namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.AutoResetEvent"/> class.
    /// </summary>
    public partial class AutoResetEventExample
    {
        #region Fields

        private AutoResetEvent autoResetEventOne;
        private AutoResetEvent autoResetEventTwo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResetEventExample"/> class.
        /// </summary>
        public AutoResetEventExample()
        {
            // set the initial state of both AutoResetEvent objects to NOT be signalled
            this.autoResetEventOne = new AutoResetEvent(false);
            this.autoResetEventTwo = new AutoResetEvent(false);
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="AutoResetEventExample"/>.
    /// </content>
    public partial class AutoResetEventExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.AutoResetEvent"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            try
            {
                Console.WriteLine("Press Enter to create three threads and start them...");
                Console.ReadKey();

                for (int i = 1; i <= 3; i++)
                {
                    Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        var name = String.Format("Task {0}", data.taskNumber);

                        Console.WriteLine(String.Format("{0} waits on AutoResetEvent #1.", name));
                        this.autoResetEventOne.WaitOne();
                        Console.WriteLine(String.Format("{0} is released from AutoResetEvent #1.", name));

                        Console.WriteLine(String.Format("{0} waits on AutoResetEvent #2.", name));
                        this.autoResetEventTwo.WaitOne();
                        Console.WriteLine(String.Format("{0} is released from AutoResetEvent #2.", name));

                        Console.WriteLine(String.Format("{0} ends.", name));
                    },
                    new { taskNumber = i });
                }

                Thread.Sleep(250);

                Console.WriteLine("All threads are now waiting on AutoResetEvent #1.");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Press Enter to release another thread.");
                    Console.ReadLine();
                    this.autoResetEventOne.Set();
                    Thread.Sleep(250);
                }

                Console.WriteLine("All threads are now waiting on AutoResetEvent #2.");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Press Enter to release a thread.");
                    Console.ReadLine();
                    this.autoResetEventTwo.Set();
                    Thread.Sleep(250);
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
