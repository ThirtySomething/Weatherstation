using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Some primitve utilities
    /// </summary>
    public class UtilsTF
    {
        /// <summary>
        /// Dump method to wait until good moment to start with
        /// </summary>
        public static void WaitUntilStart()
        {
            var currentSeconds = DateTime.Now.Second;

            while (0 < currentSeconds % 5)
            {
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