using System.Collections.Generic;
using Assets.Scripts.AI.BT;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.BTTest.Test1.Scripts
{
    public class Farmer : SimpleCharacter
    {
        private RepeatUntilFail _head;

        public HashSet<KeyValuePair<string, object>> Data;
        private Sequence _sequence;

        private void Start()
        {
            Data = new HashSet<KeyValuePair<string, object>>();

            _head = new RepeatUntilFail();
            var succeeder = _head.SetChild(new Succeeder());
            _sequence = succeeder.SetChild(new Sequence());
            _sequence.AddChild(new FindHerbBehaviour(this));
            _sequence.AddChild(new WalkToHerbBehaviour(this));
            _sequence.AddChild(new EatHerbBehaviour(this));
        }

        private void Update()
        {
            //_head.Tick();
            _sequence.Tick();
        }
    }
}
