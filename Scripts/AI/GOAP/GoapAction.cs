using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public abstract class GoapAction : MonoBehaviour
    {
        public int Cost;

        //private HashSet<KeyValuePair<string, object>> _preconditions;
        //private HashSet<KeyValuePair<string, object>> _effects;
        //public HashSet<KeyValuePair<string, object>> Preconditions
        //{
        //    get { return _preconditions; }
        //}
        //public HashSet<KeyValuePair<string, object>> Effects
        //{
        //    get { return _effects; }
        //}
        

        //protected GoapAction()
        //{
        //    _preconditions = new HashSet<KeyValuePair<string, object>>();
        //    _effects = new HashSet<KeyValuePair<string, object>>();
        //}

        public abstract void DoReset();
        public abstract bool IsDone();
        public abstract bool CheckProceduralPrecondition(GoapAgent agent);//TODO: change to coroutine
        public abstract bool Perform(GoapAgent agent);
        public abstract HashSet<KeyValuePair<string, object>> GetPreconditions();
        public abstract HashSet<KeyValuePair<string, object>> GetEffects();

        //public void AddPrecondition(string key, object value)
        //{
        //    _preconditions.Add(new KeyValuePair<string, object>(key, value));
        //}

        //public void RemovePrecondition(string key)
        //{
        //    KeyValuePair<string, object> precondToRemove = default(KeyValuePair<string, object>);
        //    foreach (var precondition in _preconditions)
        //    {
        //        if (precondition.Key.Equals(key))
        //        {
        //            precondToRemove = precondition;
        //            break;
        //        }
        //    }
        //    if (!precondToRemove.Equals(default(KeyValuePair<string, object>)))
        //        _preconditions.Remove(precondToRemove);
        //}

        //public void AddEffect(string key, object value)
        //{
        //    _effects.Add(new KeyValuePair<string, object>(key, value));
        //}

        //public void RemoveEffect(string key)
        //{
        //    KeyValuePair<string, object> effectToRemove = default(KeyValuePair<string, object>);
        //    foreach (var effect in _effects)
        //    {
        //        if (effect.Key.Equals(key))
        //        {
        //            effectToRemove = effect;
        //            break;
        //        }
        //    }
        //    if (!effectToRemove.Equals(default(KeyValuePair<string, object>)))
        //        _preconditions.Remove(effectToRemove);
        //}
    }
}