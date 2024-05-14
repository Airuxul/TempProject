using AI.HTN.Task;

namespace AI.HTN
{
    public class HTNPlanRunner
    {
        private ETaskStatus _curState;
        private readonly HTNPlanner _planner;
        private PrimitiveTask curTask;
        private bool canContinue;

        public HTNPlanRunner(HTNPlanner planner)
        {
            _planner = planner;
        }

        public void RunPlan()
        {
            switch (_curState)
            {
                case ETaskStatus.Failure:
                    _planner.Plan();
                    break;
                case ETaskStatus.Success:
                    curTask.Effect();
                    break;
            }
            if (_curState != ETaskStatus.Running)
            {
                canContinue = _planner.FinalTasks.TryPop(out curTask);
            }
            _curState = canContinue && curTask.MetCondition() ? curTask.Operator() : ETaskStatus.Failure;
        }
    }
}