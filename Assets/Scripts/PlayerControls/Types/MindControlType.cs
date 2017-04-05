using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindControlType : CharacterType {

    public RotationHandler rot;
    public Transform rotationObj;
    public Attack melee;
    public bool neutral = true;

    // Use this for initialization
    void Start()
    {
        movespeed = 3;

        name = "Hypno";
        //overwrite movespeed and hp here.
        hp = 4;
        //setting melee
    }

    public override void Die()
    {
        print("ded");
        GameObject.FindGameObjectWithTag("Unique").GetComponent<StateTrack>().Reload();

    }

    public override void Secondary()
    {
        StartCoroutine(SecondaryA());
    }




    private IEnumerator SecondaryA()
    {
        if (neutral)
        {
            neutral = false;

            float startup = .1f;
            float active = .4f;
            float recovery = .2f;
            yield return new WaitForSeconds(startup);

            //turn hitbox on.
            melee.Activate();
            yield return new WaitForSeconds(active);

            //hitbox off
            melee.Deactivate();
            yield return new WaitForSeconds(recovery);
            neutral = true;
            yield return null;
        }
        else
        {
            yield return null;
        }
    }
}
