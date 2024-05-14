using System.Collections.Generic;
using AI.HTN.Task;

namespace AI.HTN
{
    public partial class HTNPlanBuilder
    {
        private HTNPlanner _planner;
        private HTNPlanRunner _runner;
        private readonly Stack<IBaseTask> taskStack;

        public HTNPlanBuilder()
        {
            taskStack = new Stack<IBaseTask>();
        }

        private void AddTask(IBaseTask task)
        {
            if (_planner != null)
            {
                taskStack.Peek().AddNextTask(task);
            }
            else
            {
                _planner = new HTNPlanner(task as CompoundTask);
                _runner = new HTNPlanRunner(_planner);
            }

            if (task is not PrimitiveTask)
            {
                taskStack.Push(task);
            }
        }

        public void RunPlan()
        {
            _runner.RunPlan();
        }

        public HTNPlanBuilder Back()
        {
            taskStack.Pop();
            return this;
        }
        
        public HTNPlanner End()
        {
            taskStack.Clear();
            return _planner;
        }
        
        public HTNPlanBuilder CompoundTask()
        {
            AddTask(new CompoundTask());
            return this;
        }
        
        public HTNPlanBuilder Method(System.Func<bool> condition)
        {
            AddTask(new Method(condition));
            return this;
        }
    }
}