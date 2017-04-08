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

    public bool dead = false;

    //gets the max HP. Current HP is stored in the player controller.
    public int GetHP()
	{
		return hp;
	}

	public float GetMoveSpeed()
	{
		return movespeed;
	}

    public int DealDamage(int damage)
    {
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
        Destroy(gameObject);
    }

    virtual public void Primary() { }

	virtual public void Secondary() { }

    virtual public void Tertiary() { }

}
