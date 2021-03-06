﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.FSM
{
    //stack finite state machine
    public class Fsm
    {
        public delegate void State(Fsm fsm, GameObject fsmGameObject);

        private Stack<State> _stateStack = new Stack<State>();

        public void Run(GameObject fsmGameObject)
        {
            if (_stateStack.Count > 0)
                _stateStack.Peek().Invoke(this, fsmGameObject);
        }

        public void PushState(State state)
        {
            _stateStack.Push(state);
        }

        public void PopState()
        {
            _stateStack.Pop();
        }
    }
}