namespace SynchronizationPrimitives
{
    using System;
    using SynchronizationPrimitives.Examples;    

    public class Program
    {
        #region Methods

        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                // Run one example at a time by commenting out all lines except the one you want to run
                IExample example = new MonitorExample();
                //IExample example = new LockExample();
                //IExample example = new InterlockedExample();
                //IExample example = new ManualResetEventExample();
                //IExample example = new AutoResetEventExample();
                //IExample example = new CountdownEventExample();
                //IExample example = new SemaphoreExample();
                //IExample example = new MutexExample();

                example.RunExample();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception has occurred:");
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();            
        }

        #endregion
    }
}
