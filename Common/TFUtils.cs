using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Some primitve utilities
    /// </summary>
    public class TFUtils
    {
        /// <summary>
        /// Default delay for each plugin.
        /// </summary>
        public const int DefaultDelay = 5000;

        /// <summary>
        /// Returns when current timestamp seconds are divisible by number given
        /// </summary>
        /// /// <param name="divisor">The divisor which the current time must be divisible by for the thread to stop waiting</param>
        /// /// <param name="checkFrequency">Time in ms which determins how long the thread sleeps before checking the current time</param>
        public static void WaitForCleanTimestamp(int divisor, int checkFrequency)
        {
            var currentSeconds = DateTime.Now.Second;

            while (currentSeconds % divisor != 0)
            {
                WaitNMilliseconds(checkFrequency);
                currentSeconds = DateTime.Now.Second;
            }
        }

        /// <summary>
        /// Wait until n milliseconds are gone
        /// </summary>
        /// <param name="delay"></param>
        public static void WaitNMilliseconds(int delay)
        {
            System.Threading.Thread.Sleep(delay);
        }
    }
}