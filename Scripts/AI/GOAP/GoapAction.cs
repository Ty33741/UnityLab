using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Assets.Labs.AnyTest;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public abstract class GoapAction : MonoBehaviour
    {
        public int Cost;
        public Func<GoapAgent, bool> Go;

        public void DoReset()
        {
            Go = DoEnter;
            SpecificReset();
        }

        public bool IsDone()
        {
            return Go == null;
        }

        public bool DoEnter(GoapAgent agent)
        {
            bool result = Enter(agent);
            Go = Run;
            return result;
        }

        public bool DoRun(GoapAgent agent)
        {
            return Run(agent);
        }

        public bool DoExit(GoapAgent agent)
        {
            bool result = Exit(agent);
            Go = null;
            return result;
        }

        protected void ActionOver()
        {
            Go = DoExit;
        }

        protected abstract void SpecificReset();
        protected abstract bool Enter(GoapAgent agent);
        protected abstract bool Exit(GoapAgent agent);
        protected abstract bool Run(GoapAgent agent);

        public abstract bool CheckProceduralPrecondition(GoapAgent agent);//TODO: change to coroutine
        public abstract HashSet<KeyValuePair<string, object>> GetPreconditions();
        public abstract HashSet<KeyValuePair<string, object>> GetEffects();
    }
}