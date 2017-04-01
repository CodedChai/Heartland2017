using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterType : MonoBehaviour {

	protected int hp =0;
	protected float movespeed=0;

    protected float primaryRecovery  = 1f;
    protected float secondaryRecovery = .5f;
	//gets the max HP. Current HP is stored in the player controller.
	public int GetHP()
	{
		return hp;
	}

	public float GetMoveSpeed()
	{
		return movespeed;
	}
    virtual public void Primary(float joyX, float joyY) { }

	virtual public void Secondary(float joyX, float joyY )
	{	}

}
