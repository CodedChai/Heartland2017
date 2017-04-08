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
        movespeed = 3 + GlobalMoveSpeed.GetSpeedDelta();

        name = "Hypno";
        //overwrite movespeed and hp here.
        hp = 4;
        //setting melee
    }

    public override void Die()
    {

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        GameObject.Find("Canvas").GetComponentInChildren<FadeIn>().Exit();
        yield return new WaitForSeconds(.9f);
        GameObject.FindGameObjectWithTag("Unique").GetComponent<StateTrack>().Reload();
        yield return null;
    }

    public override void Secondary()
    {
        StartCoroutine(SecondaryA());
    }




    private IEnumerator SecondaryA()
    {
        PlaySound();

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

    void PlaySound()
    {
        GameObject audio = Instantiate<GameObject>(new GameObject());
        audio.AddComponent<AudioSource>();
        audio.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
