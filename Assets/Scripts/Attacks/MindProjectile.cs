using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindProjectile : MonoBehaviour
{
    public float speed;
    public float lifespan;
    public float damage;
    public float knockback;
    public float hitstun;//idk if we're going to use hitstun but it can't hurt
    float time = 0;

    public GameObject nextHost;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        //track time until we break the projectile
        time += Time.deltaTime;
        transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
        if (time > lifespan)
        {
            Destroy(gameObject);
        }
    }

    //break stuff if the move is active.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            //deal damage and stuff
            if (collision.tag == "Enemy")
            {
                //take over.
                print("I'm U");
                nextHost = collision.gameObject;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
