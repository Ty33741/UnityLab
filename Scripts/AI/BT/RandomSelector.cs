using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.AI.BT
{
    public class RandomSelector : Selector
    {
        public RandomSelector()
        {
            Update = () =>
            {
                Children.Shuffle();
                return Selecting();
            };
        }
    }
}
