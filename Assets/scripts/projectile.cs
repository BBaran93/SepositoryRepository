using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {
    public int layerToHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToHit)
        {
            if (collision.gameObject.GetComponent<entity>())
            {
                collision.gameObject.GetComponent<entity>().death();
            }
        }
    }
}
