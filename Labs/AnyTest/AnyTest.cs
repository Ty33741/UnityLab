using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Labs.AnyTest
{
    public class AnyTest : MonoBehaviour
    {
        void Start()
        {
            HashsetTest();
        }

        private HashSet<KeyValuePair<string, object>> test = new HashSet<KeyValuePair<string, object>>();

        private void HashsetTest()
        {
            AddItemToHashSet(test);
            Debug.Log(SearchInHashSet(test, "123"));
        }

        private void AddItemToHashSet(HashSet<KeyValuePair<string, object>> data)
        {
            data.Add(new KeyValuePair<string, object>("123", gameObject));
            Debug.Log(SearchInHashSet(data, "123"));
        }

        private object SearchInHashSet(HashSet<KeyValuePair<string, object>> data, string key)
        {
            foreach (var d in data)
            {
                if (d.Key.Equals(key))
                {
                    return d.Value;
                }
            }
            return null;
        }
    }
}