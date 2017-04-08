using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public int damage;
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
            print("Hitting " + col.transform.name);


            if ((col.transform.tag == "Enemy" || col.transform.tag == "Player") && col.transform.tag != tag && !hits.Contains(col.transform))//no FF and can't hit itself basically
            {

                hits.Add(col.transform);
                col.transform.GetComponent<CharacterType>().DealDamage(damage);
                float angle = GetComponentInParent<RotationHandler>().angle * Mathf.Deg2Rad;
                col.transform.Translate(new Vector3(-1* Mathf.Sin(angle),Mathf.Cos(angle)));
                //method for hitting things

            }
        }
    }

    public void Deactivate()
    {
        active = false;
    }

}
