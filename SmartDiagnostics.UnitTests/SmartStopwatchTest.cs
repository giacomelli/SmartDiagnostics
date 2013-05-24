using NUnit.Framework;
using System;
using System.Threading;

namespace SmartDiagnostics.UnitTests
{
	[TestFixture()]
	public class SmartStopwatchTest
	{
		[Test()]
		public void Stop_StartPause_TotalTimeElapsed ()
		{
			var target = new SmartStopwatch ();
			target.Start ();
			Thread.Sleep (1000);

			target.Pause ();
			Thread.Sleep (2000);

			target.Resume ();
			Thread.Sleep (1000);

			target.Pause ();
			Thread.Sleep (2000);

			target.Stop ();
			Assert.IsTrue (target.Elapsed.TotalMilliseconds >= 2000, "Should be 2000 or greater."); 
			Assert.IsTrue (target.Elapsed.TotalMilliseconds < 4000, "Should lower than 4000.");
		}

		[Test()]
		public void Start_StopStartAgain_ElapsedReseted ()
		{
			var target = new SmartStopwatch ();
			target.Start ();
			Thread.Sleep (1000);
			target.Stop ();
			Assert.IsTrue (target.Elapsed.TotalMilliseconds >= 1000); 

			target.Start ();
			target.Stop ();
			Assert.IsTrue (target.Elapsed.TotalMilliseconds <= 1000); 
		}
	}
}

