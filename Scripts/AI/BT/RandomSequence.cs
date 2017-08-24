using Assets.Scripts.Extensions;

namespace Assets.Scripts.AI.BT
{
    public class RandomSequence : Sequence
    {
        public RandomSequence()
        {
            Update = () =>
            {
                Children.Shuffle();
                return Sequencing();
            };
        }
    }
}
