using System.Collections.Generic;
using AI.HTN.Task;
using UnityEngine;

namespace AI.HTN
{
    // 注册实际Task行为
    public partial class HTNPlanBuilder
    {
        public class EatTask : PrimitiveTask
        {
            // 吃所需要的时间
            private float _eatTime = 0;
            private int _needFoodCount = 0;
            private float _startEatTime = 0;
            public EatTask(int needFoodCount, float eatTime)
            {
                _needFoodCount = needFoodCount;
                _eatTime = eatTime;
                _startEatTime = Time.time;
            }
            
            public override ETaskStatus Operator()
            {
                // 后续加上时间
                Debug.Log("发现可以吃，开吃");
                return ETaskStatus.Success;
            }

            protected override void EffectOnPlan(Dictionary<string, object> worldState)
            {
                if (worldState.TryGetValue(HTNWorldConst.FOOD_STATE_KEY, out var foodObj))
                {
                    int foodCount = (int) foodObj;
                    foodCount -= _needFoodCount;
                    worldState[HTNWorldConst.FOOD_STATE_KEY] = foodCount;
                }
            }

            protected override void EffectOnExecute()
            {
                float duration = Time.time - _startEatTime;
                int eatFoodCount = (int)(Mathf.Min(duration / _eatTime, 1) * _needFoodCount);
                HTNWorld.UpdateState(HTNWorldConst.FOOD_STATE_KEY, -eatFoodCount);
                Debug.Log($"共花费 {duration} 时间吃饭，吃了 {eatFoodCount} 个食物");
                _startEatTime = Time.time;
            }

            protected override bool MetConditionOnPlan(Dictionary<string, object> worldState)
            {
                Debug.Log("思考能不能吃");
                if (worldState.TryGetValue(HTNWorldConst.FOOD_STATE_KEY, out var foodObj))
                {
                    Debug.Log("发现食物充足");
                    int foodCount = (int) foodObj;
                    return foodCount >= _needFoodCount;
                }
                else
                {
                    Debug.Log("发现食物不够");
                    return false;
                }
            }
        }
        
        public HTNPlanBuilder EatTask_Build(int foodCount, float eatTime)
        {
            AddTask(new EatTask(foodCount, eatTime));
            return this;
        }
    }
}