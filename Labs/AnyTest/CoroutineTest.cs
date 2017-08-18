using System.Collections;
using UnityEngine;

namespace Assets.Labs.AnyTest
{
    public class CoroutineTest : MonoBehaviour
    {
        public delegate IEnumerator Func();

        private Func _func1;
        private int _count = 0;

        private void Start()
        {
            _func1 = Func1;

            StartCoroutine(_func1());

            Debug.Log("start over");
        }

        private void Update()
        {
            if (_count++ <= 20)
                Debug.Log("update");
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
            while (i++ < 20)
            {
                if (i % 2 == 0)
                    yield return null;//new WaitForSeconds(1f);
                Debug.Log(i);
            }
        }
    }
}