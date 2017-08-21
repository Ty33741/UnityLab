using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FindSmithyAction : MoveAndDoAction
    {
        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            var smithy = FindObjectOfType<Smithy>();
            if (smithy == null)
            {
                return false;
            }

            SetDestination(smithy.gameObject);
            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", false)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findSmithy", true)
            };
        }
    }
}
