using System;
using AI.HTN;
using AI.HTN.Task;
using UnityEngine;

namespace Entity
{
    public class EnityCompent : MonoBehaviour
    {
        public bool isHungry = false;

        private HTNPlanRunner _htnPlanRunner;
        
        private void Start()
        {
            HTNWorld.AddState(HTNWorldConst.FOOD_STATE_KEY, 
                () => HTNWorld.FoodCount, 
                (curFoodCount) =>
                {
                     HTNWorld.FoodCount = HTNWorld.GetState<int>(HTNWorldConst.FOOD_STATE_KEY) + (int)curFoodCount;
                });
            _htnPlanRunner = new HTNPlanRunner(new HTNPlanBuilder()
                .CompoundTask()
                .Method(() => isHungry)
                .EatTask_Build(10, 10)
                .End());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _htnPlanRunner.RunPlan();
            }
        }
    }
}