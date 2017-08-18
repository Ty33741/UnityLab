using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public interface IGoap
    {
        HashSet<KeyValuePair<string, object>> GetWorldStates();
        HashSet<KeyValuePair<string, object>> GetGoals();
        void PlanFound(HashSet<KeyValuePair<string, object>> goals, Stack<GoapAction> plan);
        void PlanFailed(HashSet<KeyValuePair<string, object>> goals);
        void PlanFinished();
        void PlanAborted(GoapAction abortedAction);
    }
}