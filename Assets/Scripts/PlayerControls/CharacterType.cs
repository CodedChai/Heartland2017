using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterType : MonoBehaviour {
    //basically an abstract class.
    public string name;
	protected int hp=4;
	protected float movespeed=3;
    public GameObject rotationTrans;
    public bool isSpecial = false;
    public bool isMelee = false;
    public bool isRanged = false;
    public bool neutral = true;
    public AudioClip clip;
    public bool dead = false;

    //gets the max HP. Current HP is stored in the player controller.
    public int GetHP()
	{
		return hp;
	}

	public float GetMoveSpeed()
	{
		return movespeed + GlobalMoveSpeed.GetSpeedDelta();
	}

    public int DealDamage(int damage)
    {
        print(name);
        hp -= damage;
        if(hp < 1)
        {
            dead = true;
            //comment this out later to allow for an animation-probably in a coroutine, then destroy it. FOr now, the dead = true thing is mostly useless.
            Die();
        }
        return hp;
    }

    virtual public void Die()
    {
        if (transform.CompareTag("Player"))
        {
            print("I died as someone else");
            GameObject p = GameObject.Find("Player");
            p.GetComponent<PlayerController>().GoBack();

        }
        GlobalMoveSpeed.AlterSpeed(1f);
        Destroy(gameObject.transform.parent.gameObject, .3f);
    }

    virtual public void Primary() { }

	virtual public void Secondary() { }

    virtual public void Tertiary() { }

    virtual public void Primary(float angle) { }

    virtual public void Secondary(float angle) { }

    virtual public void Tertiary(float angle) { }

}
