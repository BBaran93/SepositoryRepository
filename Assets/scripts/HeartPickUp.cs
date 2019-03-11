using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour {
    public AudioClip noSound;
    public AudioClip pickupSound;
    public GameObject graphic;
    AudioSource src;
	// Use this for initialization
	void Start () {
        src = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (collision.attachedRigidbody.GetComponent<player>())
            {
                player player = collision.attachedRigidbody.GetComponent<player>();
                if (!player.IsMaxHearts())
                {
                    player.addHeart();
                    graphic.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    if (src && pickupSound)
                    {
                        src.PlayOneShot(pickupSound);
                    }
                }
                else if(src && noSound)
                {
                    src.PlayOneShot(noSound);
                }
            }
        }
    }

}
