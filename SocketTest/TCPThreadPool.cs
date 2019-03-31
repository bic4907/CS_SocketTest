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
            if (currentTask == null) throw new Exception("Task Not Exist");
            Debug.Print(this.id.ToString() + " : " + "Waked up");
            if(!workerThr.IsAlive)
            {
                workerThr.Start();
            } else if(workerThr.ThreadState == System.Threading.ThreadState.Suspended)
            {
                Debug.Print(this.id.ToString() + " : " + "Resume");
                workerThr.Resume();
            }
        }
        private void workerLoop()
        {
            while (true)
            {
                Debug.Print(this.id.ToString() + " : " + currentTask.GetCommand().ToString());

                NetworkStream stream = currentTask.GetClient().GetSocket().GetStream();
                // 쓰레드가 작업할 영역
                IFormatter formatter = new BinaryFormatter();

                TCPServerMessage sendPacket = new TCPServerMessage();
                sendPacket.Cmd = currentTask.GetCommand();
                sendPacket.Param = currentTask.GetData();

                formatter.Serialize(stream, sendPacket);
   
                currentTask = null;
                Debug.Print(this.id.ToString() + " : " + "Suspended");
                workerThr.Suspend();
                
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
        private Queue<TCPTask> queue; 

        private Thread poolMgr;

        internal TCPThreadPool()
        {
            this.pool = new List<TCPThreadWorker>();
            this.queue = new Queue<TCPTask>();
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
            }
            poolMgr = new Thread(MgrLoop);
            
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
            while (true)
            {
                //Debug.Print(this.queue.Count.ToString(), this.pool.Count.ToString());

                if (this.queue.Count == 0)
                {
                    this.poolMgr.Suspend();

                }
                else
                {
                    TCPTask task = null;
                    lock (this.queue)
                    {
                        task = this.queue.Dequeue();
                    }
                    TCPThreadWorker targetWorker = null;

                    if (task == null) continue;
                    bool pushBack = false;
                    foreach (TCPThreadWorker worker in pool)
                    {
                        // TODO Task클래스에서 소켓이 사용중인지 검토필요
                        TCPTask currTask = worker.GetCurrentTask();
                        if (worker.isIdle() && task.GetClient().GetIP() == currTask.GetClient().GetIP())
                        {
                            pushBack = true;
                            break;
                        }
                        else if (worker.isIdle())
                        {
                            targetWorker = worker;
                            break;
                        }
                    }
                    if (targetWorker == null) pushBack = true;

                    if (pushBack)
                    {
                        this.queue.Enqueue(task);
                        continue;
                    }
                    targetWorker.Push(task);
                    targetWorker.Start();
                }
                
            }
        }

        public void Push(TCPTask task)
        {
            lock (this.queue)
            {
                this.queue.Enqueue(task);
            }
            if (!poolMgr.IsAlive)
            {
                poolMgr.Start();
            } else if(poolMgr.ThreadState == System.Threading.ThreadState.Suspended)
            {
                poolMgr.Resume();
            }

        }




    }
}
