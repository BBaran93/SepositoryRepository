using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingEnemy : enemy
{
    public float shootInterval = 1;
    protected override void Start()
    {
        base.Start();
        StartCoroutine("Attack");

    }
    IEnumerator Attack()
    {
        while (!dead) 
        {
            yield return new WaitForSeconds(shootInterval);
            shoot();
        }
    }
}
