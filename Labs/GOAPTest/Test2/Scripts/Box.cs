using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class Box : MonoBehaviour
    {
        public int Count;

        private GameObject _item;

        private void Start()
        {
            _item = transform.Find("Item").gameObject;
            Debug.Assert(_item != null);

            ResetItem();
        }

        protected void AddItem(int x)
        {
            Count += x;
            ResetItem();
        }

        protected bool ConsumeItem(int x)
        {
            if (Count < x)
            {
                return false;
            }

            Count -= x;
            return true;
        }

        private void ResetItem()
        {
            _item.SetActive(Count > 0);
        }
    }
}