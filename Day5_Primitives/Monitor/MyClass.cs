using System.Threading;

namespace Monitor
{
    // TODO: Use Monitor (not lock) to protect this structure.
    public class MyClass
    {
        private int _value;
        private object syncObject = new object();

        public int Counter
        {
            get
            {
                return _value;
            }
            set
            {
                System.Threading.Monitor.Enter(syncObject);
                try
                {
                    _value = value;
                }
                finally
                {
                    System.Threading.Monitor.Exit(syncObject);
                }
            }
        }

        public void Increase()
        {
            System.Threading.Monitor.Enter(syncObject);
            try
            {
                _value++;
            }
            finally
            {
                System.Threading.Monitor.Exit(syncObject);
            }
            
        }

        public void Decrease()
        {
            System.Threading.Monitor.Enter(syncObject);
            try
            {
                _value--;
            }
            finally
            {
                System.Threading.Monitor.Exit(syncObject);
            }
        }
    }
}
