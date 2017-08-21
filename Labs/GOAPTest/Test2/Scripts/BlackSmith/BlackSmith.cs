using System;
using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class BlackSmith : SimpleGoapCharacter
    {
        public int Hunger;
        public int MaxHunger;
        public ProgressBar HungerBar;
        public GameObject Tool;
        public int CraftCost = 20;
        public int EatRecovery = 90;

        private Func<HashSet<KeyValuePair<string, object>>> _goal;

        private void Start()
        {
            _goal = Goal1;
            HungerBar.SetRate((float)Hunger / MaxHunger);
        }

        public void EatFood()
        {
            Hunger += EatRecovery;
            if (Hunger > MaxHunger)
            {
                Hunger = MaxHunger;
            }
            HungerBar.SetRate((float)Hunger / MaxHunger);
        }

        public void ConsumeFood()
        {
            Hunger -= CraftCost;
            if (Hunger < 0)
            {
                Hunger = 0;
            }
            HungerBar.SetRate((float)Hunger / MaxHunger);
        }

        public void EquipTool()
        {
            Tool.SetActive(true);
        }

        public void DestroyTool()
        {
            Tool.SetActive(false);
        }

        public override HashSet<KeyValuePair<string, object>> GetWorldStates()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hunger", Hunger < CraftCost),
                new KeyValuePair<string, object>("hasTool", Tool.activeInHierarchy),
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetGoals()
        {
            return _goal();
        }

        private HashSet<KeyValuePair<string, object>> Goal1()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectTool", true)
            };
        }

        private HashSet<KeyValuePair<string, object>> Goal2()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("eatFood", true)
            };
        }

        public override void PlanFailed(HashSet<KeyValuePair<string, object>> goals)
        {
            _goal = Goal2;
            base.PlanFailed(goals);
        }

        public override void PlanFinished()
        {
            _goal = Goal1;
            base.PlanFinished();
        }
    }
}
