using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : entity
{    
    public float jumpCount = 0;
    public int maxHearts = 3;
    private int currentHearts;
    public List<GameObject> UIHearts = new List<GameObject>();
    // Use this for initialization
    public Vector3 resetPoint;
    public LayerMask boxlayer;
    public float bumpForceUp = 50;
    public float bumpForceHorizontal = 100;
    public float invincibilityTime = 3;
    protected override void Start()
    {
        base.Start();
        resetPoint = transform.position;
        currentHearts = maxHearts;
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        anim.SetBool("isGrounded", isGrounded);    
	}

    public override void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        base.Move();
        anim.SetBool("isMoving", isMoving);
    }
    public override void jump()
    {
        base.jump();
        jumpCount++;
    }

    public override void onGroundHit()
    {
        base.onGroundHit();
        jumpCount = 0;
    }
    public override void death()
    {
        base.death();
        currentHearts--;
        UIHearts[currentHearts].SetActive(false);
        if (currentHearts <= 0)
        {
            transform.position = resetPoint;
            ResetHearts();
        }
    }
    void ResetHearts()
    {
        currentHearts = maxHearts;
        foreach (GameObject heart in UIHearts)
        {
            heart.SetActive(true);
        }
    }
    public bool IsMaxHearts()
    {
        return currentHearts == maxHearts;
    }
    public void addHeart()
    {
        if (currentHearts < maxHearts)
        {
            UIHearts[currentHearts].SetActive(true);
            currentHearts++;
        }
    }
    public override void onEnemyHit(GameObject enemy)
    {
        base.onEnemyHit(enemy);
        BoxCollider2D mycollider = GetComponent<BoxCollider2D>();
        BoxCollider2D enemycollider = GetComponent<BoxCollider2D>();
        float heightDifference = transform.position.y - enemy.transform.position.y;


        RaycastHit2D hit = Physics2D.BoxCast(transform.position, mycollider.size * 0.99f, 0, Vector2.down, 1, boxlayer);
        if (transform.position.y > enemy.transform.position.y&&(hit && hit.collider.gameObject == enemy))
        {
            enemy.GetComponent<entity>().death();
        }
        else
        {
            Vector3 force = new Vector3();
            force.x = Mathf.Sign(transform.position.x - enemy.transform.position.x) * bumpForceHorizontal;
            force.y = bumpForceUp;
            rb.AddForce(force);
            StopAllCoroutines();
            StartCoroutine("MakeInvincible");
            death();
        }
    }

    IEnumerator MakeInvincible()
    {
        gameObject.layer = 11;//invince layer
        StartCoroutine("PlayFlashEffect");
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 9;//player layer
        StopCoroutine("PlayFlashEffect");
        anim.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator PlayFlashEffect()
    {
        bool toggle = false;
        while(true)
        {
            anim.gameObject.GetComponent<SpriteRenderer>().enabled = toggle;
            yield return new WaitForSeconds(0.2f);
            toggle = !toggle;
        }
    }
   
}
