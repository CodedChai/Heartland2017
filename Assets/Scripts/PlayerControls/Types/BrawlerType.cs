using System.Collections;
using UnityEngine;

public class BrawlerType : CharacterType
{

    bool neutral = true;//check if player can do stuff

    public Attack melee;//the script associated w/ the melee attack
    public Attack bigMelee;
  

    // Use this for initialization
    void Start()
    {
        name = "Boxer";
        //overwrite movespeed and hp here.
        movespeed = 4;
        hp = 6;
        //setting melee
        isSpecial = false;
        isMelee = true;
        isRanged = false;
}


    //secondary attack, melee hit. B is either big or smol
    public override void Secondary()
    {
        print("Melee first attack");
        StartCoroutine(SecondaryA());
    }

    //thrid attack, the projectile. Y also melee
    public override void Tertiary()
    {
        print("Melee secondary attack");
        StartCoroutine(TertiaryA());
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
            neutral = false;

            float startup = .4f;
            float active = .6f;
            float recovery = .1f;
            yield return new WaitForSeconds(startup);

            //turn hitbox on.
            bigMelee.Activate();
            yield return new WaitForSeconds(active);

            //hitbox off
            bigMelee.Deactivate();
            yield return new WaitForSeconds(recovery);
            neutral = true;
            yield return null;
        }
        else
        {
            yield return null;
        }

    }
    //secondary attack, melee hit. B is either big or smol
    public override void Secondary(float angle)
    {
        print("Melee first attack");
        StartCoroutine(SecondaryA(angle));
    }

    //thrid attack, the projectile. Y also melee
    public override void Tertiary(float angle)
    {
        print("Melee secondary attack");
        StartCoroutine(TertiaryA(angle));
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
            neutral = false;

            float startup = .4f;
            float active = .6f;
            float recovery = .1f;
            yield return new WaitForSeconds(startup);

            //turn hitbox on.
            bigMelee.Activate();
            yield return new WaitForSeconds(active);

            //hitbox off
            bigMelee.Deactivate();
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
}
