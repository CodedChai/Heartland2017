using System.Collections;
using UnityEngine;

public class Teleport : CharacterType{

    bool neutral = true;//check if player can do stuff

    public GameObject at;//gameObject containing hitbox of the move-I couldn't drag the script to it, so we get it from a gameObject at the start
    public Attack melee;//the script associated w/ the melee attack

    public GameObject projectile;//the projectile.

	// Use this for initialization
	void Start()
    {
        //overwrite movespeed and hp here.
        movespeed = 3;
        hp = 4;

        //setting melee
        melee = at.GetComponent<Attack>();
    }

    //Primary attack. A-Teleports.
    override public void Primary(float joyX, float joyY)
    {
        StartCoroutine(PrimaryA(joyX, joyY));
    }

    //secondary attack, melee hit. B
    public override void Secondary(float joyX, float joyY)
    {
        StartCoroutine(SecondaryA(joyX, joyY));
    }

    //thrid attack, the projectile. Y
    public override void Tertiary(float joyX, float joyY)
    {
        StartCoroutine(TertiaryA(joyX, joyY));
    }

    //primary attack: a teleport.
    private IEnumerator PrimaryA(float joyX, float joyY)
    {
        if (neutral)
        {
            neutral = false;
            float startup = .1f;
            float invul = .4f;
            float recovery = .2f;
            float distance = 3;

            //set up teleport point
            Vector3 futPos = transform.position + new Vector3(joyX * distance, joyY * distance);
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

    private IEnumerator SecondaryA(float joyX, float joyY)
    {
        if (neutral)
        {
            neutral = false;

            //get angle of strike
            float angle = Mathf.Atan2(joyY, joyX) * Mathf.Rad2Deg - 90f;

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

    private IEnumerator TertiaryA(float joyX, float joyY)
    {
        if (neutral)
        {
            //get aim dir
            neutral = false;
            float angle = Mathf.Atan2(joyY, joyX) * Mathf.Rad2Deg - 90f;

            float startup = .1f;
            yield return new WaitForSeconds(startup);

            //create projectile and set its position and parents
            GameObject bullet = Instantiate(projectile, transform);
            bullet.transform.localEulerAngles = new Vector3(0, 0, angle);
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
