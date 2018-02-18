using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Test_IHSMarkit.arf_.arf_parallel
{
    public class arf_Distributor<T>
    {
        private BlockingCollection<T> blockingCollection = null;
        private Action<T, Encoding> action = null;
        private Action<T> dwn_action = null;
        private Task[] tasks = null;
        private Encoding encoding = null;
        public arf_Distributor()
        { }

        public arf_Distributor(Action<T, Encoding> _action, List<T> _list, Encoding _encoding)
        {
            action = _action;
            encoding = _encoding;
            blockingCollection = new BlockingCollection<T>();
            foreach (var i in _list)
                blockingCollection.Add(i);
            blockingCollection.CompleteAdding();
        }
        public arf_Distributor(Action<T> _action, List<T> _list)
        {
            dwn_action = _action;
            blockingCollection = new BlockingCollection<T>();
            foreach (var i in _list)
                blockingCollection.Add(i);
            blockingCollection.CompleteAdding();
        }

        public void Start(Int32 countTask, Boolean dwn = false)
        {
            tasks = new Task[countTask];
            for(Int32 i = 0; i < countTask; ++i)
            {
                if (!dwn)
                    tasks[i] = new Task(_function);
                else
                    tasks[i] = new Task(dwn_function);
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
        }
        private void _function()
        {
            while (!blockingCollection.IsCompleted)
            {
                action.Invoke(blockingCollection.Take(), encoding);
            }
        }
        private void dwn_function()
        {
            while (!blockingCollection.IsCompleted)
            {
                dwn_action.Invoke(blockingCollection.Take());
            }
        }
    }
}
