using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FindToolAction : MoveAndDoAction
    {
        private ToolBox _toolBox;
        private Farmer _farmer;

        protected override void Start()
        {
            _farmer = GetComponent<Farmer>();
            Debug.Assert(_farmer != null);
            base.Start();
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            _toolBox = FindObjectOfType<ToolBox>();
            if (_toolBox == null || _toolBox.Count == 0)
            {
                return false;
            }

            SetDestination(_toolBox.gameObject);

            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            _toolBox.ConsumeTool(1);
            _farmer.EquipTool();
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
                new KeyValuePair<string, object>("findFood", false),
                new KeyValuePair<string, object>("hasTool", true)
            };
        }
    }
}

