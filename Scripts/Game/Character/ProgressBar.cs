using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    public class ProgressBar : MonoBehaviour
    {
        public GameObject Bar;
        public float Value;
        public float MaxValue = 100;

        private float _scale;
        private float _size;

        private void Start()
        {
            _scale = Bar.transform.localScale.x;
            var sprite = Bar.GetComponent<SpriteRenderer>().sprite;
            _size = sprite.rect.width / sprite.pixelsPerUnit * _scale;
        }

        private void Update()
        {
            var x = _scale * Value / MaxValue;
            SetScale(x);

            float offset = _size * (1 - Value / MaxValue) / 2;
            SetPosition(offset);
        }

        public void SetRate(float rate)
        {
            Value = MaxValue * rate;
        }

        public void SetValue(float v)
        {
            Value = v;
        }

        private void SetScale(float x)
        {
            var scale = Bar.transform.localScale;
            scale.x = x;
            Bar.transform.localScale = scale;
        }

        private void SetPosition(float offset)
        {
            var pos = Bar.transform.localPosition;
            pos.x = -offset;
            Bar.transform.localPosition = pos;
        }
    }
}
