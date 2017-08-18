using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Labs.AnyTest
{
    public class AnyTest : MonoBehaviour
    {
        void Start()
        {
            //int[] a = { 1, 2, 3 };
            //int[] b = Test(a);
            //foreach (var i in a)
            //{
            //    Debug.Log(i);
            //}
            //foreach (var i in b)
            //{
            //    Debug.Log(i);
            //}

            //List<int> a = new List<int>();
            //a.Add(1);
            //a.Add(2);
            //a.Add(3);
            //List<int> b = Test(a);
            //foreach (var i in a)
            //{
            //    Debug.Log(i);
            //}
            //foreach (var i in b)
            //{
            //    Debug.Log(i);
            //}

            string a = "123";
            string b = Test(ref a);
            Debug.Log(a);
            Debug.Log(b);
        }

        private int[] Test(int[] test)
        {
            test[0] = 9;
            return test.Where(i => i != 2).ToArray();
        }

        private List<int> Test(List<int> test)
        {
            test.RemoveAt(1);
            return test;
        }

        private string Test(ref string test)
        {
            test = test.Insert(0, "x");
            return test;
        }
    }
}