using System.Collections;
using UnityEngine;

public class GunnerType : CharacterType
{

    bool neutral = true;//check if player can do stuff

    public GameObject projectile;//the script associated w/ the melee attack
    public GameObject bigProjectile;//the projectile.

    // Use this for initialization
    void Start()
    {
        name = "Shooter";
        //overwrite movespeed and hp here.
        movespeed = 2;
        hp = 2;
        //setting ranged
        isSpecial = false;
        isMelee = true;
        isRanged = true;
    }

    //secondary attack, melee hit. B
    public override void Secondary()
    {
        print("Gunner melee attack");
        StartCoroutine(SecondaryA());
    }

    //thrid attack, the projectile. Y
    public override void Tertiary()
    {
        print("Gunner ranged attack");
        StartCoroutine(TertiaryA());
    }

    //normal projectile
    private IEnumerator SecondaryA()
    {
        PlaySound();

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
        PlaySound();

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


    //secondary attack, melee hit. B
    public override void Secondary(float angle)
    {
        print("Gunner melee attack");
        StartCoroutine(SecondaryA(angle));
    }

    //thrid attack, the projectile. Y
    public override void Tertiary(float angle)
    {
        print("Gunner ranged attack");
        StartCoroutine(TertiaryA(angle));
    }

    //normal projectile
    private IEnumerator SecondaryA(float angle)
    {
        PlaySound();

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

    //big projectile
    private IEnumerator TertiaryA(float angle)
    {
        PlaySound();
        transform.localEulerAngles = new Vector3(0f, 0f, angle);

        if (neutral)
        {
            //get aim dir
            neutral = false;

            float startup = .3f;
            yield return new WaitForSeconds(startup);

            //create projectile and set its position and parents
            GameObject bullet = Instantiate(bigProjectile, transform);
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

    void PlaySound()
    {
        GameObject audio = Instantiate<GameObject>(new GameObject());
        audio.AddComponent<AudioSource>();
        audio.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
