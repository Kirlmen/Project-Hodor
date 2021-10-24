using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticle;
    Transform target;




    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void AimWeapon()
    //aim the top mesh to the closest enemy
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);
        if (targetDistance < range)
        {
            Attack(true);
        }
        else { Attack(false); }

    }

    void Attack(bool isActive) //particle system on and off
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
