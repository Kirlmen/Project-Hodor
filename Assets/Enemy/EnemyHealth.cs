using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int difficultyRamp = 1; //ramping the health after an enemy dies +1
    int currentHitPoints = 0;
    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        Debug.Log(currentHitPoints);
        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
