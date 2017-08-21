using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class DropToolAction : MoveAndDoAction
    {
        private ToolBox _toolBox;
        private BlackSmith _blackSmith;

        protected override void Start()
        {
            _blackSmith = GetComponent<BlackSmith>();
            base.Start();
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            _toolBox = FindObjectOfType<ToolBox>();
            if (_toolBox == null)
            {
                return false;
            }
            
            SetDestination(_toolBox.gameObject);

            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            _toolBox.AddTool(1);
            _blackSmith.DestroyTool();

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", true)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", false),
                new KeyValuePair<string, object>("collectTool", true)
            };
        }
    }
}
