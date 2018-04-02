using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Some primitve utilities
    /// </summary>
    public class TFUtils
    {
        /// <summary>
        /// Returns when current timestamp seconds are divisible by number given
        /// </summary>
        public static void WaitForCleanTimestamp(int divisor, int checkFrequency)
        {
            var currentSeconds = DateTime.Now.Second;

            while (currentSeconds % divisor != 0)
            {
                System.Threading.Thread.Sleep(checkFrequency);
                currentSeconds = DateTime.Now.Second;
            }
        }
    }
}