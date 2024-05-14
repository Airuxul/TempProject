using System.Collections.Generic;

namespace AI.HTN.Task
{
    public class CompoundTask : IBaseTask
    {
        public Method ValidMethod { get; private set; }
        private readonly List<Method> _methods;

        public CompoundTask()
        {
            _methods = new List<Method>();
        }
            
        public bool MetCondition(Dictionary<string, object> worldState)
        {
            foreach (var method in _methods)
            {
                if (method.MetCondition(worldState))
                {
                    ValidMethod = method;
                    return true;
                }
            }

            return false;
        }

        public void AddNextTask(IBaseTask nextTask)
        {
            if (nextTask is Method m)
            {
                _methods.Add(m);
            }
        }
    }
}