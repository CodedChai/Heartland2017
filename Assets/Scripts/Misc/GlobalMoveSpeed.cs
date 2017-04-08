using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMoveSpeed : MonoBehaviour
{
    private static GlobalMoveSpeed _instance;
    public float moveSpeedIncrease = 0f;

    private static GlobalMoveSpeed instance
    {
    get
    {
        //if the instance is null, first make sure there's not already a gameobject named MoneySystem. If there is, check for the
        //MoneySystem component and set it as instance, otherwise add the component and set the new one as instance.
        // If there isn't a gameobject named MoneySystem, make one and add the MoneySystem component.
        //Lastly, return the instance.
        if (_instance == null)
        {
            if (GameObject.Find("GlobalMoveSpeed"))
            {

                GameObject g = GameObject.Find("GlobalMoveSpeed");
                if (g.GetComponent<GlobalMoveSpeed>())
                {
                    _instance = g.GetComponent<GlobalMoveSpeed>();
                }
                else
                {
                    _instance = g.AddComponent<GlobalMoveSpeed>();
                }
            }
            else
            {
                GameObject g = new GameObject();
                g.name = "GlobalMoveSpeed";
                _instance = g.AddComponent<GlobalMoveSpeed>();
            }
        }
        return _instance;
    }

    set
    {
        _instance = value;
    }
}

    public static void AlterSpeed(float delta)
    {
        instance.moveSpeedIncrease = instance.moveSpeedIncrease + delta;
    }


    public static float GetSpeedDelta()
    {
        return instance.moveSpeedIncrease;
    }

    // Use this for initialization
    void Start()
{
    // Make sure the Gameobject is named MoneySystem
    gameObject.name = "GlobalMoveSpeed";
    _instance = this;
}
}

