using System.Collections.Generic;

namespace AI.Task
{
    public enum ETaskStatus
    {
        Ready,
        Running,
        Success,
        Failure
    }
    
    public interface IBaseTask
    {
        bool MetCondition(Dictionary<string, object> worldState);
        void AddNextTask(IBaseTask nextTask);
    }
}