using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test1.Scripts
{
    public class Farmer : SimpleCharacter, IGoap
    {
        public int ToolCount = 3;
        public int HerbCount = 0;
        public int HerbNeeded = 3;

        public HashSet<KeyValuePair<string, object>> GetWorldStates()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectHerb", HerbCount >= HerbNeeded),
                new KeyValuePair<string, object>("hasTool", ToolCount > 0)
            };
        }

        public HashSet<KeyValuePair<string, object>> GetGoals()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectHerb", true)
            };
        }

        public void PlanFound(HashSet<KeyValuePair<string, object>> goals, Stack<GoapAction> plan)
        {
            int cost = 0;
            foreach (var action in plan)
            {
                cost += action.Cost;
            }

            string str = "<color=green>Plan found</color>\n";
            str += "[ " + GoapAgent.PrettyPlan(plan) + " ]";
            str += " --> [ " + GoapAgent.PrettyGoals(goals) + " ]\n";
            str += "cost: " + cost.ToString();
            Debug.Log(str);
        }

        public void PlanFailed(HashSet<KeyValuePair<string, object>> goals)
        {
            string str = "<color=red>Plan failed</color>\n";
            str += "[ " + GoapAgent.PrettyGoals(goals) + " ]";
            Debug.Log(str);
        }

        public void PlanFinished()
        {
            string str = "<color=green>Plan finished</color>\n";
            Debug.Log(str);
        }

        public void PlanAborted(GoapAction abortedAction)
        {
            string str = "<color=blue>Plan aborted</color>\n";
            str += abortedAction.GetType().Name + "\n";
            Debug.Log(str);
        }
    }
}