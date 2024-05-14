using System.Collections.Generic;
using AI.HTN.Task;

namespace AI.HTN
{
    public class HTNPlanner
    {
        public Stack<PrimitiveTask> FinalTasks { get; private set; }
        private readonly Stack<IBaseTask> _taskOfProcess;
        private readonly CompoundTask _rootTask;
        
        public HTNPlanner(CompoundTask rootTask)
        {
            _rootTask = rootTask;
            _taskOfProcess = new Stack<IBaseTask>();
            FinalTasks = new Stack<PrimitiveTask>();
        }
        
        public void Plan()
        {
            var worldState = HTNWorld.CopyWorldState();
            FinalTasks.Clear();
            _taskOfProcess.Push(_rootTask);
            
            while (_taskOfProcess.Count > 0)
            {
                var currentTask = _taskOfProcess.Pop();
                if (currentTask is CompoundTask compoundTask)
                {
                    if (compoundTask.MetCondition(worldState))
                    {
                        var subTask = compoundTask.ValidMethod.SubTasks;
                        foreach (var t in subTask)
                        {
                            _taskOfProcess.Push(t);
                        }
                    }
                }
                else
                {
                    var primitiveTask = currentTask as PrimitiveTask;
                    FinalTasks.Push(primitiveTask);
                }
            }
        }
    }
}