using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class CraftToolAction : GoapAction
    {
        public float Duration = 3f;

        private BlackSmith _blackSmith;
        private ProgressBar _bar;
        private float _time;

        private void Start()
        {
            _blackSmith = GetComponent<BlackSmith>();
            _bar = GetComponent<ProgressBar>();
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _time = 0;
            _blackSmith.ConsumeFood();

            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            _time += Time.deltaTime;
            _bar.SetRate(_time / Duration);
            if (_time > Duration)
            {
                RunOver();
            }

            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            _bar.SetValue(0);
            _blackSmith.EquipTool();
            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hunger", false),
                new KeyValuePair<string, object>("findSmithy", true)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("hasTool", true)
            };
        }
    }
}
