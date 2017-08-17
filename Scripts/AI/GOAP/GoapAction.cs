using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public abstract class GoapAction : MonoBehaviour
    {
        private HashSet<KeyValuePair<string, object>> _preconditions;
        private HashSet<KeyValuePair<string, object>> _effects;

        public int Cost;
        [SerializeField] private GameObject _target;
        public bool IsInRange { get; set; }
        public bool IsDone { get; set; }
        
        public HashSet<KeyValuePair<string, object>> Preconditions
        {
            get { return _preconditions; }
        }

        public HashSet<KeyValuePair<string, object>> Effects
        {
            get { return _effects; }
        }

        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }
        
        protected GoapAction()
        {
            _preconditions = new HashSet<KeyValuePair<string, object>>();
            _effects = new HashSet<KeyValuePair<string, object>>();
            DoReset();
        }

        protected abstract void SpecificReset();
        public abstract IEnumerator CheckProceduralPrecondition(GameObject agent);
        public abstract bool Perform(GameObject agent);
        public abstract bool RequireInRange();//这个和Action本身息息相关，所以应该用abstract-override写好

        public void DoReset()
        {
            IsInRange = false;
            IsDone = false;
            SpecificReset();
        }

        public void AddPrecondition(string key, object value)
        {
            _preconditions.Add(new KeyValuePair<string, object>(key, value));
        }

        public void RemovePrecondition(string key)
        {
            KeyValuePair<string, object> precondToRemove = default(KeyValuePair<string, object>);
            foreach (var precondition in _preconditions)
            {
                if (precondition.Key.Equals(key))
                {
                    precondToRemove = precondition;
                    break;
                }
            }
            if (!precondToRemove.Equals(default(KeyValuePair<string, object>)))
                _preconditions.Remove(precondToRemove);
        }

        public void AddEffect(string key, object value)
        {
            _effects.Add(new KeyValuePair<string, object>(key, value));
        }

        public void RemoveEffect(string key)
        {
            KeyValuePair<string, object> effectToRemove = default(KeyValuePair<string, object>);
            foreach (var effect in _effects)
            {
                if (effect.Key.Equals(key))
                {
                    effectToRemove = effect;
                    break;
                }
            }
            if (!effectToRemove.Equals(default(KeyValuePair<string, object>)))
                _preconditions.Remove(effectToRemove);
        }
    }
}