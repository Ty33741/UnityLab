using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class FoodSpawner : MonoBehaviour
    {
        public GameObject FoodPrefab;
        public int FoodLimit = 3;
        public float SpawnInterval = 1f;
        public float SpawnRange = 1f;

        private float _nextSpawnTime = 0;

        private void Update()
        {
            if (Time.time > _nextSpawnTime)
            {
                Food[] foods = FindObjectsOfType<Food>();
                if (foods.Length < FoodLimit)
                {
                    float x = Random.Range(-SpawnRange, SpawnRange);
                    float y = Random.Range(-SpawnRange, SpawnRange);
                    GameObject food = Instantiate(FoodPrefab);
                    food.transform.position = transform.position + new Vector3(x, y, 0);
                }
                _nextSpawnTime += SpawnInterval;
            }
        }
    }
}