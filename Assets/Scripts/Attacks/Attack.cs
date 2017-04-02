using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public float damage;
    public float knockback;
    public float hitstun;//not sure if we want this or not, but yeah.

    public bool active = false;
	// activate and deactivate the move
    public void Activate()
    {
        active = true;
    }

    //do stuff if in active frames
    void OnTriggerStay2D(Collider2D col)
    {
        if(active)
        {
            print(col.transform.name);

            if (col.transform.tag == "Enemy")
            {
                //method for hitting things

            }
        }
    }

    public void Deactivate()
    {
        active = false;
    }

}
