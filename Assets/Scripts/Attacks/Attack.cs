using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public int damage;
    public float knockback;
    public float hitstun;//not sure if we want this or not, but yeah.

    public List<Transform> hits;

    public bool active = false;
	// activate and deactivate the move
    public void Activate()
    {
        hits = new List<Transform>();
        active = true;
    }

    //do stuff if in active frames
    void OnTriggerStay2D(Collider2D col)
    {
        if(active)
        {

            if (col.transform.tag == "Enemy" && !hits.Contains(col.transform))
            {
                print(col.transform.name);

                hits.Add(col.transform);
                col.transform.GetComponent<CharacterType>().DealDamage(damage);
                //method for hitting things

            }
        }
    }

    public void Deactivate()
    {
        active = false;
    }

}
