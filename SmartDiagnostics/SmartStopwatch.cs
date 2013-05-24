using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SmartDiagnostics
{
    /// <summary>
	/// Stopwatch allowing Pause and Resume, enabling the recording of internal points, as loops.
    /// <example>
    /// <code>
    /// var swFirstLoop = new SmartStopwatch();
    /// var swSecondLoop = new SmartStopwatch();
    /// 
    /// for(int i = 0; i < 10; i++)
    /// {
    ///     swFirstLoop.Resume();
	///     // Perform some processing within the first loop.
    ///     swFirstLoop.Pause();
    ///     
    ///     for(int  j = 0; j < 100; j++)
    ///     {
    ///         swSecondLoop.Resume();
	///         // Perform some processing within the second loop.
    ///         swSecondLoop.Pause();
    ///     }
    /// }
    /// 
    /// swFirstLoop.Stop();
    /// swSecondLoop.Stop();
    /// </code>
	/// When checking the output window of Visual Studio / Xamarin Studio you can see the sum of the time spent in all iterations of the processing performed in the first and second loop.
    /// </example>
    /// </summary>
    public class SmartStopwatch
    {
        #region Fields
        /// <summary>
		/// Counter used to identify instances Stopwatch without OutputIdentifier.
        /// </summary>
        private static int s_instancesCounter;

        /// <summary>
		/// The internal Stopwatch that performs the basic work.
        /// </summary>
        private Stopwatch m_stopWatch;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartDiagnostics.SmartStopwatch"/> class.
        /// </summary>
        public SmartStopwatch()
        {
            s_instancesCounter++;
            OutputIdentifier = s_instancesCounter.ToString();
            Elapsed = new TimeSpan(0);
            OutputPauseElapsedEnabled = false;
            OutputStopElapsedEnabled = true;
            m_stopWatch = new Stopwatch();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartDiagnostics.SmartStopwatch"/> class.
        /// </summary>
		/// <param name="outputIdentifier">The identifier of the Stopwatch in the output .</param>
        public SmartStopwatch(string outputIdentifier) : this()
        {
            OutputIdentifier = outputIdentifier;
        }
        #endregion

        #region Properties
        /// <summary>
		/// Gets the total elapsed time from the first Start / Resume, considering the Pause and Resume performed. 
        /// </summary>
        public TimeSpan Elapsed { get; private set; }

        /// <summary>
		/// Gets or sets the identifier of the instance in the output window.
        /// </summary>
        public string OutputIdentifier { get; set; }

        /// <summary>
		/// Gets or sets if the time between the last Start / Resume should be recorded in the output window when executes Pause.
        /// </summary>
        public bool OutputPauseElapsedEnabled { get; set; }

        /// <summary>
		///Gets or sets if the total elapsed time should be recorded in the output window when executes Stop.
        /// </summary>
        public bool OutputStopElapsedEnabled { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
		/// Start the time recording.
        /// </summary>
        public void Start()
        {            
            Elapsed = new TimeSpan(0);
            Resume();
        }

        /// <summary>
        /// Pauses the time recording.
        /// </summary>
        public string Pause()
        {
            m_stopWatch.Stop();
            Elapsed += m_stopWatch.Elapsed;

			var msg = GetOutputMessage("Pause", true);
            Debug.WriteLineIf(OutputPauseElapsedEnabled, msg);

			return msg;
        }

        /// <summary>
        /// Resumes the time recording.
		/// <remarks>Can be used directly, without having to call the Start method before.</remarks>
        /// </summary>
        public void Resume()
        {
            m_stopWatch = new Stopwatch();            
            m_stopWatch.Start();
        }

        /// <summary>
		/// Completely stop the elapsed time record.
        /// </summary>
        public string Stop()
        {
            m_stopWatch.Stop();
            Elapsed += m_stopWatch.Elapsed;

			var msg = GetOutputMessage("Stop", false);
            Debug.WriteLineIf(OutputStopElapsedEnabled, msg);

			return msg;
        }
        #endregion

        #region Private Methods
        /// <summary>
		/// Creates the default message for output.
        /// </summary>
        /// <param name="actionName">Action name: Pause, Stop, etc.</param>
		/// <param name="outputPartialElapsed">Whether to log the part time elapsed.</param>
		/// <returns>The formatted message.</returns>
        private string GetOutputMessage(string actionName, bool outputPartialElapsed)
        {
            if (outputPartialElapsed)
            {
                return String.Format("{0};{1};{2};{3}", OutputIdentifier, actionName, m_stopWatch.Elapsed, Elapsed);
            }
            else
            {
                return String.Format("{0};{1};{2}", OutputIdentifier, actionName, Elapsed);
      
            }
        }
        #endregion
    }
}
