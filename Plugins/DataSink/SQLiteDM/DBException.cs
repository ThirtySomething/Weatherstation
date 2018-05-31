using System;
using System.Collections.Generic;
using System.Text;

namespace net.derpaul.tf.plugin
{
    public class DBException : Exception
    {
        public DBException()
        {
        }

        public DBException(string message)
            : base(message)
        {
        }

        public DBException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
