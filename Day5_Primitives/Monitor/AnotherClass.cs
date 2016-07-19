using System.Threading;

namespace Monitor
{
    // TODO: Use SpinLock to protect this structure.
    public class AnotherClass
    {
        private int _value;
        private SpinLock spinLock = new SpinLock();

        public int Counter
        {
            get
            {
                bool lockTaken = false;
                try
                {
                    spinLock.Enter(ref lockTaken);
                    return _value;
                }
                finally
                {
                    if (lockTaken)
                        spinLock.Exit();
                }
            }
            set
            {
                _value = value;
            }
        }

        public void Increase()
        {
            bool lockTaken = false;
            try
            {
                spinLock.Enter(ref lockTaken);
                _value++;
            }
            finally
            {
                if (lockTaken)
                    spinLock.Exit();
            }
        }

        public void Decrease()
        {
            bool lockTaken = false;
            try
            {
                spinLock.Enter(ref lockTaken);
                _value--;
            }
            finally
            {
                if (lockTaken)
                    spinLock.Exit();
            }
        }
    }
}
