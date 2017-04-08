using System.Collections;
using UnityEngine;

public class GunnerType : CharacterType
{

    bool neutral = true;//check if player can do stuff

    public GameObject projectile;//the script associated w/ the melee attack
    public GameObject bigProjectile;//the projectile.

    public bool isSpecial = false;
    public bool isMelee = true;
    public bool isRanged = true;

    // Use this for initialization
    void Start()
    {
        name = "Shooter";
        //overwrite movespeed and hp here.
        movespeed = 2;
        hp = 2;
        //setting melee
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

    //normal projectile
    private IEnumerator SecondaryA()
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

    //big projectile
    private IEnumerator TertiaryA()
    {
        if (neutral)
        {
            //get aim dir
            neutral = false;

            float startup = .3f;
            yield return new WaitForSeconds(startup);

            //create projectile and set its position and parents
            GameObject bullet = Instantiate(bigProjectile, transform);
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
}
