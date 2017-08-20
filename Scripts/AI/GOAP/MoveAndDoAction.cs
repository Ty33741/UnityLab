using System.Collections.Generic;
using Assets.Labs.GOAPTest.Test2.Scripts;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    // simple action: move to target and do something
    public class MoveAndDoAction : GoapAction
    {
        public GameObject DestTarget;

        private SimpleCharacter _moveable;

        protected virtual void Start()
        {
            _moveable = GetComponent<SimpleCharacter>();
            Debug.Assert(_moveable != null);
        }

        protected void SetDestination(GameObject target)
        {
            DestTarget = target;
        }

        protected bool Move()
        {
            return _moveable.MoveTo(DestTarget.transform.position, _moveable.MoveSpeed, _moveable.Range);
        }

        public override bool CheckProceduralPrecondition(GoapAgent agent)
        {
            return DestTarget != null;
        }

        protected override bool Enter(GoapAgent agent)
        {
            return true;
        }

        protected override bool Run(GoapAgent agent)
        {
            if (Move())
            {
                RunOver();
            }
            return true;
        }

        protected override bool Exit(GoapAgent agent)
        {
            return true;
        }

        public override HashSet<KeyValuePair<string, object>> GetPreconditions()
        {
            return new HashSet<KeyValuePair<string, object>>();
        }

        public override HashSet<KeyValuePair<string, object>> GetEffects()
        {
            return new HashSet<KeyValuePair<string, object>>();
        }
    }
}
