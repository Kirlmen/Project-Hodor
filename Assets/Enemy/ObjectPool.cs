﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;
    private void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectsInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }

        }
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(spawnTimer);

        }
    }

}
