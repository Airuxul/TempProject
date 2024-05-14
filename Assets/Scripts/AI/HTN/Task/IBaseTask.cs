using System.Collections.Generic;

namespace AI.HTN.Task
{
    public enum ETaskStatus
    {
        Failure, 
        Success, 
        Running
    }
    
    public interface IBaseTask
    {
        bool MetCondition(Dictionary<string, object> worldState);
        void AddNextTask(IBaseTask nextTask);
    }
}