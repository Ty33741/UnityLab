using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class PickupFoodAction : GoapAction
    {
        public float Duration = 3f;

        protected ProgressBar _bar;
        protected float _time;

        protected void Start()
        {
            _bar = GetComponent<ProgressBar>();
            Debug.Assert(_bar != null);
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            return true;
        }

        protected override bool Enter(GoapAgent agent)
        {
            _time = 0;
            
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

            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findFood", true)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("holdFood", true)
            };
        }
    }
}