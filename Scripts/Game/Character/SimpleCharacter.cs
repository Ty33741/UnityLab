using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    //简单人物基类
    public class SimpleCharacter : MonoBehaviour, ICharacter
    {
        public float MoveSpeed = 4f;
        public float Range = 1f;

        public bool MoveTo(Vector3 dest, float speed, float range = 0.1f)
        {
            if ((dest - transform.position).magnitude < range)
            {
                return true;
            }

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dest, step);

            return false;
        }
    }
}