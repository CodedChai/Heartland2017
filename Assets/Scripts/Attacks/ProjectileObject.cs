using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour {
    public Transform og;
    public float speed;
    public float lifespan;
    public int damage;
    public float hitstun;//idk if we're going to use hitstun but it can't hurt
    float time = 0;
    // Use this for initialization

    // Update is called once per frame
    private void Start()
    {
    }
    void Update ()
    {
        //track time until we break the projectile
        time += Time.deltaTime;
        transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
        if(time > lifespan)
        {
            Destroy(gameObject);
        }
	}

    //break stuff if the move is active.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != og && !collision.CompareTag("Attack"))
        {
            if (collision.GetComponent<CharacterType>())
            {
                collision.GetComponent<CharacterType>().DealDamage(damage);
                transform.position = collision.transform.position;
                collision.transform.position = transform.position;
            }

            Destroy(gameObject);
        }
    }
}
