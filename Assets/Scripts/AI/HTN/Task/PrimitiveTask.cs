using System.Collections.Generic;

namespace AI.HTN.Task
{
    public abstract class PrimitiveTask : IBaseTask
    {
        public abstract ETaskStatus Operator();
        
        public bool MetCondition(Dictionary<string, object> worldState = null)
        {
            if (worldState == null)
            {
                return MetConditionOnExecute();
            }
            else
            {
                if (MetConditionOnPlan(worldState))
                {
                    EffectOnPlan(worldState);
                    return true;
                }
            }

            return false;
        }

        void IBaseTask.AddNextTask(IBaseTask nextTask)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual bool MetConditionOnPlan(Dictionary<string, object> worldState)
        {
            return true;
        }
        
        protected virtual bool MetConditionOnExecute()
        {
            return true;
        }
        
        public void Effect(Dictionary<string, object> worldState = null)
        {
            EffectOnExecute();
        }
        
        protected virtual void EffectOnPlan(Dictionary<string, object> worldState) { }
        
        protected virtual void EffectOnExecute() { }
    }
}