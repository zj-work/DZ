using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Extend.Tasks
{
    public class TaskBuilder
    {
        private static TaskFactory _instance = null;
        private static readonly object locker = new object();

        public static TaskFactory GetFactory(int parallelCount = 20)
        {
            //设置最高可以并行100个线程
            LimitedConcurrencyLevelTaskScheduler lcts = new LimitedConcurrencyLevelTaskScheduler(parallelCount);
            if (_instance == null)
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new TaskFactory(lcts);
                    }
                }
            }
            return _instance;
        }
    }
}
