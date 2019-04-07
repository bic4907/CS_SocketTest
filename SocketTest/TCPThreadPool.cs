using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            workerThr = new Thread(workerLoop);

        }
        public bool isIdle()
        {
            return currentTask == null;
        }

        public int Id { get => id; set => id = value; }


        public void Start()
        {
            workerThr.Start();
        }
        private void workerLoop()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (currentTask == null)
                {
                    continue;
                }

                NetworkStream stream = currentTask.GetClient().GetSocket().GetStream();
                // 쓰레드가 작업할 영역
                lock (stream)
                {
                    IFormatter formatter = new BinaryFormatter();

                    TCPServerMessage sendPacket = new TCPServerMessage();
                    sendPacket.Cmd = currentTask.GetCommand();
                    sendPacket.Param = currentTask.GetData();

                    formatter.Serialize(stream, sendPacket);

                    currentTask = null;
                }
                
            }
        }
        public void Stop()
        {
            workerThr.Abort();
            workerThr = null;
        }
        public void Push(TCPTask task)
        {
            this.currentTask = task;
        }
        public TCPTask GetCurrentTask()
        {
            return currentTask;
        }


    }

    class TCPThreadPool
    {
        private static TCPThreadPool instance;
        private static int ThreadCount = 10;
        private List<TCPThreadWorker> pool;
        private LinkedList<TCPTask> queue; 

        private Thread poolMgr;

        internal TCPThreadPool()
        {
            this.pool = new List<TCPThreadWorker>();
            this.queue = new LinkedList<TCPTask>();
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
        public void Start()
        {            
            foreach (int i in Enumerable.Range(0, ThreadCount))
            {
                Debug.Print(i.ToString());
                this.pool.Add(new TCPThreadWorker(i));
                this.pool.Last().Start();
            }
            poolMgr = new Thread(MgrLoop);
            poolMgr.Start();
        }

        public void Stop()
        {
            this.queue.Clear();
            poolMgr.Abort();

            foreach (TCPThreadWorker worker in pool)
            {
                worker.Stop();
                pool.Remove(worker);
            }
        }

        void MgrLoop()
        {
            while (true)
            {
                //Debug.Print(this.queue.Count.ToString(), this.pool.Count.ToString());
                Thread.Sleep(1);
                if (this.queue.Count == 0)
                {
                    continue;
                }
                else
                {
                    TCPTask task = null;
                    lock (this.queue)
                    {
                        task = this.queue.First();
                    }
                    TCPThreadWorker targetWorker = null;
                    if (task == null) continue;
                    bool pushBack = false;

                    if (!task.GetClient().GetSocket().GetStream().CanWrite) pushBack = true;
                    if (pushBack)
                    {
                        this.queue.AddFirst(task);
                        continue;
                    }

                    foreach (TCPThreadWorker worker in pool)
                    {
                        if (worker.isIdle())
                        {
                            targetWorker = worker;
                            break;
                        }
                    }
                    if (targetWorker == null) pushBack = true;

                    if (pushBack)
                    {
                        this.queue.AddFirst(task);
                        continue;
                    }
                    targetWorker.Push(task);
                    this.queue.RemoveFirst();
                }
                
            }
        }

        public void Push(TCPTask task)
        {
            lock (this.queue)
            {
                this.queue.AddFirst(task);
            }


        }




    }
}
