using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    public interface ICharacter
    {
        bool MoveTo(Vector3 dest, float speed);
    }
}
