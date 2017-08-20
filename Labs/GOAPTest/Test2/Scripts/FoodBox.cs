using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FoodBox : Box
    {
        public void AddFood(int x)
        {
            AddItem(x);
        }

        public bool ConsumeFood(int x)
        {
            return ConsumeItem(x);
        }
    }
}