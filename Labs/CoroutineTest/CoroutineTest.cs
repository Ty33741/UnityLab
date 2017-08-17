using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Labs.CoroutineTest
{
    public class CoroutineTest : MonoBehaviour
    {
        public delegate IEnumerator Func();

        private Func _func1;

        private void Start()
        {
            _func1 = Func1;

            StartCoroutine(_func1());
            
            Debug.Log("start over");
        }

        public IEnumerator Func1()
        {
            yield return StartCoroutine(Func2());
            Debug.Log("func1 over");
            yield return null;
        }

        public IEnumerator Func2()
        {
            int i = 0;
            while (i++ < 3)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log(i);
            }
        }
    }
}