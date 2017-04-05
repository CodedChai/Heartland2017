using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateTrack : MonoBehaviour {


    // Use this for initialization

    public int maxUnlocked;
    public int curLevel;
    public List<string> levelNames;//list of level names. HARDCODE THESE

    void Start ()
    {
        GameObject[] posers = GameObject.FindGameObjectsWithTag("Unique");
        foreach(GameObject poser in posers)
        {
            if(poser != this.gameObject)
            {
                Destroy(poser);
            }
        }
        DontDestroyOnLoad(gameObject);


    }




    public void LoadLevel()
    {
        if (curLevel != levelNames.Count - 1)//if it's not a valid level.
        {
            if (curLevel == maxUnlocked)
            {
                maxUnlocked++;
            }
            curLevel++;
            LoadLevel(curLevel);
        }
        else
        {
            SceneManager.LoadScene("Test");
        }
    }

    public void LoadLevel(int level)
    {
        if (level <= maxUnlocked)
        {
            SceneManager.LoadScene(levelNames[level]);
        }

    }

    public void Reload()
    {
        SceneManager.LoadScene(levelNames[curLevel]);
    }

    public void LoadLevelUnchecked(int level)
    {
        SceneManager.LoadScene(levelNames[level]);
    }



	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("l"))
        {
            LoadLevel();
        }
        if (Input.GetKeyDown("k"))
        {
            Reload();
        }
	}
}
