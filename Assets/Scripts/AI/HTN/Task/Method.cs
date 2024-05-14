using System;
using System.Collections.Generic;

namespace AI.HTN.Task
{
    public class Method : IBaseTask
    {
        public List<IBaseTask> SubTasks { get; private set; }
        private readonly Func<bool> _condition;
        
        public Method(Func<bool> condition)
        {
            SubTasks = new List<IBaseTask>();
            _condition = condition;
        }
        
        public bool MetCondition(Dictionary<string, object> worldState)
        {
            var tpWorld = new Dictionary<string, object>(worldState);
            if (_condition())
            {
                foreach (var t in SubTasks)
                {
                    if (!t.MetCondition(tpWorld))
                    {
                        return false;
                    }
                }
                worldState = tpWorld;
                return true;
            }

            return false;
        }

        public void AddNextTask(IBaseTask nextTask)
        {
            SubTasks.Add(nextTask);
        }
    }
}