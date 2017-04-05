using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);
        if(collision.transform.tag == "Player" && collision.transform.GetComponent<MindControlType>())
            GameObject.FindGameObjectWithTag("Unique").GetComponent<StateTrack>().LoadLevel();
    }
}
