using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public class DataResender
    {
        private bool IsLock = true;

        private event Action onDied;
        private Recall recall;

        private int maxCallingDeathTime;
        private int waitingTime;

        public DataResender(Action onDied)
        {
            this.onDied = onDied;
        }

        public DataResender(Action onDied, int maxCallingDeathTime, int waitingTime)
        {
            this.onDied = onDied;

            Resetting(maxCallingDeathTime, waitingTime);
        }

        private void CreateRecall()
        {
            if (maxCallingDeathTime <= 0 || waitingTime <= 0)
            {
                recall = new Recall();
            }
            else
            {
                recall = new Recall(maxCallingDeathTime, waitingTime);
            }
        }

        public void Resetting(int maxCallingDeathTime, int waitingTime)
        {
            if (maxCallingDeathTime <= 0 || waitingTime <= 0)
                throw new Exception("Field's \"maxCallingDeathTime\" or \"waitingTime\" can't be less than or equal zero!");

            this.maxCallingDeathTime = maxCallingDeathTime;
            this.waitingTime = waitingTime;
        }

        public bool Start(Action sendMethod)
        {
            if (!IsLock)
                return false;

            Lock();
            CreateRecall();
            recall.Start(sendMethod, Die);

            return true;
        }

        public void Stop()
        {
            recall.Stop();
            Unlock();
        }

        private void Lock()
        {
            IsLock = false;
        }

        private void Unlock()
        {
            IsLock = true;
        }

        private void Die()
        {
            Unlock();
            onDied();
        }
    }
}
