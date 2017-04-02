using System.Collections;
using UnityEngine;

public class TeleportType : CharacterType{

    bool neutral = true;//check if player can do stuff

    public GameObject attackObj;//gameObject containing hitbox of the move-I couldn't drag the script to it, so we get it from a gameObject at the start
    public Attack melee;//the script associated w/ the melee attack
    public Transform rotationTrans;
    public GameObject projectile;//the projectile.

	// Use this for initialization
	void Start()
    {
        //overwrite movespeed and hp here.
        movespeed = 3;
        hp = 4;
        rotationTrans = transform.Find("Rotation");
        //setting melee
        melee = attackObj.GetComponent<Attack>();
    }

    //Primary attack. A-Teleports.
    override public void Primary()
    {
        StartCoroutine(PrimaryA());
    }

    //secondary attack, melee hit. B
    public override void Secondary()
    {
        StartCoroutine(SecondaryA());
    }

    //thrid attack, the projectile. Y
    public override void Tertiary()
    {
        StartCoroutine(TertiaryA());
    }

    //primary attack: a teleport.
    private IEnumerator PrimaryA()
    {
        if (neutral)
        {
            neutral = false;
            float startup = .1f;
            float invul = .4f;
            float recovery = .2f;
            float distance = 3;

            //set up teleport point
            Vector3 futPos = transform.position + new Vector3(rotationTrans.eulerAngles.x * distance, rotationTrans.eulerAngles.y * distance);
            yield return new WaitForSeconds(startup);
            
            //remove hitboxes or set invul as true for now.
            yield return new WaitForSeconds(invul);
            
            //actually teleport
            transform.position = futPos;
            yield return new WaitForSeconds(recovery);

            //you can do other stuff now.
            neutral = true;
            yield return null;
        }
        else
        {
            yield return null;
        }
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

    private IEnumerator TertiaryA()
    {
        if (neutral)
        {
            //get aim dir
            neutral = false;

            float startup = .1f;
            yield return new WaitForSeconds(startup);

            //create projectile and set its position and parents
            GameObject bullet = Instantiate(projectile, transform);
            bullet.transform.eulerAngles = rotationTrans.eulerAngles;
            bullet.transform.SetParent(null);
            neutral = true;
            yield return null;
        }
        else
        {
            yield return null;
        }

    }
}
