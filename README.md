SmartDiagnostics
================
[![Build Status](https://travis-ci.org/giacomelli/SmartDiagnostics.png?branch=master)](https://travis-ci.org/giacomelli/SmartDiagnostics)

Extensions for better perform disgnostic on .NET code


NuGet
=======
PM> Install-Package SmartDiagnostics

Usage
=======
Nowadays there is only one class:

**SmartStopwatch**

Stopwatch allowing Pause and Resume, enabling the recording of internal points, as loops.


```c#

var swFirstLoop = new SmartStopwatch();
var swSecondLoop = new SmartStopwatch();

for(int i = 0; i < 10; i++)
{
	swFirstLoop.Resume();
	// Perform some processing within the first loop.

	swFirstLoop.Pause();
     for(int  j = 0; j < 100; j++)
     {
         swSecondLoop.Resume();
         // Perform some processing within the second loop.
         swSecondLoop.Pause();
     }
}

swFirstLoop.Stop();
swSecondLoop.Stop();
```

When checking the output window of Visual Studio / Xamarin Studio you can see the sum of the time spent in all iterations of the processing performed in the first and second loop.