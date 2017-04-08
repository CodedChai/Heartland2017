using System.Collections;
using UnityEngine;

public class TeleportType : CharacterType{

    bool neutral = true;//check if player can do stuff

    public Attack melee;//the script associated w/ the melee attack
    public GameObject projectile;//the projectile.

	// Use this for initialization
	void Start()
    {
        name = "Phil";
        //overwrite movespeed and hp here.
        movespeed = 2;
        hp = 2;
        //setting tele
        isSpecial = true;
        isMelee = true;
        isRanged = true;
    }

    //Primary attack. A-Teleports.
    override public void Primary()
    {
        print("Special teleport");
        StartCoroutine(PrimaryA());
    }

    //secondary attack, melee hit. B
    public override void Secondary()
    {
        print("Special melee attack");
        StartCoroutine(SecondaryA());
    }

    //thrid attack, the projectile. Y
    public override void Tertiary()
    {
        print("Special projectile attack");
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

            //using these because going back and forth between rotation and doing trig sounds hard


             LayerMask mask = 1<<8;

            //set up teleport point
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rotationTrans.transform.up, distance, mask);
            if (hit.collider != null)
            {
                distance = Mathf.Abs(hit.point.y - transform.position.y);
            }

            Vector3 futPos = transform.position +rotationTrans.transform.up * distance;

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
            bullet.GetComponent<ProjectileObject>().og = transform;
            bullet.transform.eulerAngles = rotationTrans.transform.eulerAngles;
            bullet.transform.SetParent(null);
            neutral = true;
            yield return null;
        }
        else
        {
            yield return null;
        }

    }

    override public void Primary(float angle)
    {
        print("Special teleport");
        StartCoroutine(PrimaryA(angle));
    }

    //secondary attack, melee hit. B
    public override void Secondary(float angle)
    {
        print("Special melee attack");
        StartCoroutine(SecondaryA(angle));
    }

    //thrid attack, the projectile. Y
    public override void Tertiary(float angle)
    {
        print("Special projectile attack");
        StartCoroutine(TertiaryA(angle));
    }

    //primary attack: a teleport.
    private IEnumerator PrimaryA(float angle)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, angle);

        if (neutral)
        {
            neutral = false;
            float startup = .1f;
            float invul = .4f;
            float recovery = .2f;
            float distance = 3;

            //using these because going back and forth between rotation and doing trig sounds hard


            LayerMask mask = 1 << 8;
            //set up teleport point
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localEulerAngles, distance, mask);
            if (hit.collider != null)
            {
                distance = Mathf.Abs(hit.point.y - transform.position.y);
            }

            Vector3 futPos = transform.position + transform.localEulerAngles * distance;

            yield return new WaitForSeconds(startup);
            //remove hitboxes or set invul as true for now.
            yield return new WaitForSeconds(invul);

            //actually teleport
            transform.position = futPos;
            yield return new WaitForSeconds(recovery);

            //you can do other stuff now.
            neutral = true;
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }
    }

    private IEnumerator SecondaryA(float angle)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, angle);

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
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }
    }

    private IEnumerator TertiaryA(float angle)
    {
        transform.localEulerAngles = new Vector3(0f, 0f, angle);

        if (neutral)
        {
            //get aim dir
            neutral = false;

            float startup = .1f;
            yield return new WaitForSeconds(startup);

            //create projectile and set its position and parents
            GameObject bullet = Instantiate(projectile, transform);
            bullet.GetComponent<ProjectileObject>().og = transform;
            bullet.transform.SetParent(null);
            neutral = true;
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            yield return null;
        }

    }
}
