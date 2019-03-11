using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entity : MonoBehaviour {
    //movment
    public float speed = 10;
    public float jumpForce = 5;
    protected float horizontal = 0;
    protected bool isMoving;
    protected float vertical = 4;
    //shoot
    public GameObject projectile;
    public Transform spawnPoint;
    public float projectileForce;

    //ground check
    protected Rigidbody2D rb;
    protected bool isGrounded;

    //animations
    public Animator anim;

    //death
    public bool dead = true;

    //enemy
    public int enemyLayer;

    //Audio
    public AudioSource src;
    public AudioClip jumpAudio;
    public AudioClip shootAudio;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        src = GetComponent<AudioSource>();
    }
   public float GetHorizontalSpeed()
    {
        return speed * horizontal;
    }
    
    public float GetVerticalSpeed()
    {
        return rb.velocity.y;
    }
    public virtual void Move()
    {
        transform.position += speed * horizontal * Vector3.right * Time.deltaTime;
        if (horizontal > 0.05f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isMoving = true;
        }
        else if (horizontal < -0.05f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
     public virtual void jump()
    {
        isGrounded = false;
        rb.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
        if (jumpAudio != null && src != null)
        {
            src.volume = 0.3f;
            src.PlayOneShot(jumpAudio);
        }
    }
     public virtual void shoot()
    {
        GameObject spawnedProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce * transform.right, ForceMode2D.Impulse);
        if (shootAudio != null && src != null)
        {
            src.volume = 1f;
            src.PlayOneShot(shootAudio);
        }
    }


    public virtual void onGroundHit()
    {
        isGrounded = true;
    }

    public virtual void onEnemyHit(GameObject enemy)
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            onGroundHit();
        }
        if (collision.gameObject.layer == enemyLayer)
        {
            onEnemyHit(collision.gameObject);
        }


        
    }
    public virtual void death()
    {

    }
}
