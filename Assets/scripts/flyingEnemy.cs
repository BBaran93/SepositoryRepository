using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : enemy
{
    public override void Move()
    {
        base.Move();
        if (transform.position.y < 1)
        {
            vertical = 1;
        }
        else if (transform.position.y > 2)
        {
            vertical = -1;
        }

        transform.position -= 0.5f * Vector3.down * Time.deltaTime * vertical;
    }
}
