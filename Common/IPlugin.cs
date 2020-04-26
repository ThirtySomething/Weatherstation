using System;
using System.Collections.Generic;
using System.Text;

namespace net.derpaul.tf
{
    public interface IPlugin
    {
        /// <summary>
        /// Flags successful initialization
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// Enable plugin to shutdown some resources
        /// </summary>
        void Shutdown();
    }
}
