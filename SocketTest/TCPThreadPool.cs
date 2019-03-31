using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTest
{
    class TCPThreadWorker
    {
        private int id;

        private TCPTask currentTask;
        private Thread workerThr;
        

        internal TCPThreadWorker(int i)
        {
            this.id = i;
        }
        public bool hasWork()
        {
            return currentTask == null;
        }

        public int Id { get => id; set => id = value; }


        public void Start()
        {
            workerThr = new Thread(workerLoop);
            workerThr.Start();
        }
        private void workerLoop()
        {
            while(true)
            {
                if (currentTask == null) continue;

                // 쓰레드가 작업할 영역


                currentTask = null;
            }
        }
        public void Stop()
        {
            workerThr.Abort();
        }
        public void Push(TCPTask task)
        {
            this.currentTask = task;
        }


    }

    class TCPThreadPool
    {
        private static TCPThreadPool instance;
        public static int ThreadCount = 10;
        public List<TCPThreadWorker> pool;
        public Queue<TCPTask> queue; 

        private Thread poolMgr;

        internal TCPThreadPool()
        {
            
        }
        ~TCPThreadPool()
        {

        }

        public static TCPThreadPool GetInstance()
        {
            if (instance == null)
            {
                instance = new TCPThreadPool();
            }
            return instance;
        }
        public static TCPThreadPool GetInstance(int cnt)
        {
            if (instance == null)
            {
                instance = new TCPThreadPool();
            }
            ThreadCount = cnt;
            return instance;
        }
        public void Initialize()
        {
            this.pool = new List<TCPThreadWorker>();
            this.queue = new Queue<TCPTask>();

            foreach (int i in Enumerable.Range(0, ThreadCount))
            {
                this.pool.Add(new TCPThreadWorker(i));
            }
            poolMgr = new Thread(MgrLoop);
            poolMgr.Start();
        }

        public void Stop()
        {
            this.queue.Clear();
            if (poolMgr != null)
            {
                poolMgr.Abort();
            }

            foreach (TCPThreadWorker worker in pool)
            {
                worker.Stop();
                pool.Remove(worker);
            }
        }

        void MgrLoop()
        {
            while(true)
            {
                if (this.queue.Count == 0) continue;
                TCPTask task = this.queue.Dequeue();
                TCPThreadWorker targetWorker = null;

                bool pushBack = false;
                foreach (TCPThreadWorker worker in pool)
                {
                    if (worker.hasWork() && task.GetClient().GetIP() == task.GetClient().GetIP())
                    {
                        pushBack = true;
                        break;
                    } else if(!worker.hasWork())
                    {
                        targetWorker = worker;
                        break;
                    }
                }
                if (targetWorker == null) pushBack = true;

                if(pushBack) {
                    this.queue.Enqueue(task);
                    continue;
                }

                targetWorker.Push(task);
            }
        }

        void Push(TCPTask task)
        {
            this.queue.Enqueue(task);
        }




    }
}
