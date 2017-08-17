using UnityEngine;

namespace Assets.Labs.FSMTest.Scripts
{
    public class HerbSpawner : MonoBehaviour
    {
        public GameObject HerbPrefab;
        public float MaxOffset;
        public GUISkin Skin;

        private void OnGUI()
        {
            GUI.skin = Skin;

            if (GUILayout.Button("spawn herb"))
            {
                float offsetX = Random.Range(-MaxOffset, MaxOffset);
                float offsetY = Random.Range(-MaxOffset, MaxOffset);

                GameObject herb = Instantiate(HerbPrefab);
                herb.transform.position = transform.position + new Vector3(offsetX, offsetY, 0);
            }
        }
    }
}