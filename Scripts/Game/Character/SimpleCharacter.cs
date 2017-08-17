using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    //简单人物基类
    public class SimpleCharacter : MonoBehaviour, ICharacter
    {
        public bool MoveTo(Vector3 dest, float speed)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dest, step);

            return (dest - transform.position).magnitude < 0.1f;
        }
    }
}