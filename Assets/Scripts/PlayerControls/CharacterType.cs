using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterType : MonoBehaviour {
    //basically an abstract class.
    public string name;
	protected int hp = 0;
	protected float movespeed=0;
    public GameObject rotationTrans;


    //gets the max HP. Current HP is stored in the player controller.
    public int GetHP()
	{
		return hp;
	}

	public float GetMoveSpeed()
	{
		return movespeed;
	}

    virtual public void Primary() { }

	virtual public void Secondary() { }

    virtual public void Tertiary() { }

}
