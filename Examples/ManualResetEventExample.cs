namespace SynchronizationPrimitives.Examples
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example class illustrating the use of the <see cref="System.Threading.ManualResetEvent"/> class.
    /// </summary>
    public partial class ManualResetEventExample
    {
        #region Fields

        private ManualResetEvent manualResetEvent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualResetEventExample"/> class.
        /// </summary>
        public ManualResetEventExample()
        {
            this.manualResetEvent = new ManualResetEvent(false);
        }

        #endregion
    }

    /// <content>
    /// <see cref="IExample"/> implementation for <see cref="ManualResetEventExample"/>.
    /// </content>
    public partial class ManualResetEventExample : IExample
    {
        #region Methods

        /// <summary>
        /// Runs the example for the <see cref="System.Threading.ManualResetEvent"/> class.
        /// </summary>
        public void RunExample()
        {
            Console.WriteLine(String.Format("Executing {0}", this.GetType().Name));

            try
            {
                Console.WriteLine("Start three tasks that block on a ManualResetEvent");
                for (int i = 0; i <= 2; i++)
                {
                    Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        var name = String.Format("Task {0}", data.taskNumber);
                        Console.WriteLine(String.Format("{0} starts and calls ManualResetEvent.WaitOne()", name));
                        this.manualResetEvent.WaitOne();
                        Console.WriteLine(String.Format("{0} ends", name));
                    },
                    new { taskNumber = i });
                }

                Thread.Sleep(500);
                Console.WriteLine("All three tasks have started, press the Enter key to call ManualResetEvent.Set() to release all the waiting tasks.");
                Console.ReadKey();

                this.manualResetEvent.Set();

                Thread.Sleep(500);
                Console.WriteLine("When we signal a ManualResetEvent, any tasks or threads that call WaitOne() do not block. Press Enter to see an example.");
                Console.ReadKey();

                for (int i = 3; i <= 4; i++)
                {
                    Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        var name = String.Format("Task {0}", data.taskNumber);
                        Console.WriteLine(String.Format("{0} starts and calls ManualResetEvent.WaitOne()", name));
                        this.manualResetEvent.WaitOne();
                        Console.WriteLine(String.Format("{0} ends", name));
                    },
                    new { taskNumber = i });
                }

                Thread.Sleep(500);
                Console.WriteLine("Press Enter to call ManualResetEvent.Reset(), so that threads once again block when they call WaitOne().");
                Console.ReadLine();

                this.manualResetEvent.Reset();

                Task.Factory.StartNew((Object obj) =>
                {
                    var data = (dynamic)obj;
                    var name = String.Format("Task {0}", data.taskNumber);
                    Console.WriteLine(String.Format("{0} starts and calls ManualResetEvent.WaitOne()", name));
                    this.manualResetEvent.WaitOne();
                    Console.WriteLine(String.Format("{0} ends", name));
                },
                new { taskNumber = 5 });

                Thread.Sleep(500);
                Console.WriteLine("Press Enter to call ManualResetEvent.Set() and conclude the demo.");
                Console.ReadLine();

                manualResetEvent.Set();
            }
            catch (AggregateException ex)
            {
                Helper.HandleAggregateException(ex);
            }
        }

        #endregion
    }
}
