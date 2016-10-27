# Carlson Garage MQTT Client

This project contains some examples that showcase the usage of synchronization primitives in the System.Threading class in .NET.  While these examples are build with .NET Core in mind, they should work well on any version of .NET

The only prerequisite is that you have .NET installed, Core or Full edition.
Once you clone the repository, you can run it by executing the following commands in a terminal prompt:

        dotnet restore
        dotnet build
        dotnet run

If you look at Program.cs in the "Main()" function, you'll see this:

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

Simply comment out every "IExample" line except the one you want to see, and run the project.
Feel free to reach out to me at Twitter via [@carlsoncoder](https://twitter.com/carlsoncoder "@carlsoncoder") with any questions, or check out [my blog.](http://www.carlsoncoder.com/ "Carlson Coder Blog")

Thanks,
Justin

License
----
All code in this repository that was created by me is considered [Beerware](http://en.wikipedia.org/wiki/Beerware "Beerware")

