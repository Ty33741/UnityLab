using System.Collections.Generic;
using Assets.Scripts.Game.Character;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    // simple and standard character with GOAP, just inherit it
    public class SimpleGoapCharacter : SimpleCharacter, IGoap
    {
        public bool ShowPlanState;

        public virtual HashSet<KeyValuePair<string, object>> GetWorldStates()
        {
            return new HashSet<KeyValuePair<string, object>>();
        }

        public virtual HashSet<KeyValuePair<string, object>> GetGoals()
        {
            return new HashSet<KeyValuePair<string, object>>();
        }

        public virtual void PlanFound(HashSet<KeyValuePair<string, object>> goals, Stack<GoapAction> plan)
        {
            if (ShowPlanState)
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
        }

        public virtual void PlanFailed(HashSet<KeyValuePair<string, object>> goals)
        {
            if (ShowPlanState)
            {
                string str = "<color=red>Plan failed</color>\n";
                str += "[ " + GoapAgent.PrettyGoals(goals) + " ]";
                Debug.Log(str);
            }
        }

        public virtual void PlanFinished()
        {
            if (ShowPlanState)
            {
                string str = "<color=green>Plan finished</color>\n";
                Debug.Log(str);
            }
        }

        public virtual void PlanAborted(GoapAction abortedAction)
        {
            if (ShowPlanState)
            {
                string str = "<color=blue>Plan aborted</color>\n";
                str += abortedAction.GetType().Name + "\n";
                Debug.Log(str);
            }
        }
    }
}
