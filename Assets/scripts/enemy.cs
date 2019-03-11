using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : entity
{
    public Transform raycast;
    public float allowedFallDistance = 1;
    public LayerMask groundLayer;
    public float deathForce = 3;

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
        horizontal = -1;
    }
    protected virtual void Update ()
    {
        if (!dead)
        {
            Move();
        }    
	}
    public override void Move()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast.position, Vector2.down, allowedFallDistance, groundLayer);
        if (!(hit && hit.collider != null))
        {
            horizontal = -horizontal;
        }
        base.Move();
    }
    public override void death()
    {
        anim.SetBool("death", true);
        dead = true;
        Destroy(GetComponent<BoxCollider2D>());     
        jump();
    }
}
