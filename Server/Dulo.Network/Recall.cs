using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public class Recall
    {
        private Action sendMethod;

        private event Action onDied;

        private Task task;

        public int MaxCallingDeathTime { get; set; } = 3;
        public int WaitingTime { get; set; } = 3000;

        private int numberTry = 0;

        private bool IsStopped = false;

        public Recall() { }

        public Recall(int maxCallingDeathTime, int waitingTime)
        {
            MaxCallingDeathTime = maxCallingDeathTime;
            WaitingTime = waitingTime;
        }

        public void Start(Action sendMethod, Action onDied)
        {
            this.sendMethod = sendMethod;
            this.onDied = onDied;

            task = new Task(CheckRecall);

            sendMethod();
            task.Start();
        }

        private void Zeroing()
        {
            numberTry = 0;
            IsStopped = false;
        }

        private void CheckRecall()
        {
            while (true)
            {
                Thread.Sleep(WaitingTime);

                if (IsStopped)
                {
                    return;
                }

                if (numberTry >= MaxCallingDeathTime)
                {
                    Die();
                    return;
                }

                numberTry++;
                sendMethod();
            }
        }

        private void Die()
        {
            Zeroing();
            onDied.Invoke();
        }

        public void Stop()
        {
            IsStopped = true;
        }
    }
}
