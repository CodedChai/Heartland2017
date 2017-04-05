using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
            if(poser != gameObject)
            {
                Destroy(poser);
            }
        }
        DontDestroyOnLoad(gameObject);

        Load();

    }




    public void LoadLevel()
    {
        if (curLevel != levelNames.Count - 1)//if it's not a valid level.
        {
            if (curLevel == maxUnlocked)
            {
                maxUnlocked++;
                Save();

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

    public string Save()
    {
        return Save("save.dat");
    }

    public string Save(string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + name);
        LoadData ld = new LoadData();
        ld.Save(maxUnlocked);
        bf.Serialize(file, ld);
        file.Close();
        return name;
    }

    public void Load()
    {
        Load("save.dat");
    }

    public void Load(string name)
    {
        if(File.Exists(Application.persistentDataPath +"/"+name))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name, FileMode.Open);
            LoadData load = (LoadData)bf.Deserialize(file);
            maxUnlocked = load.maxLevel;
            file.Close();
        }
        else
        {
            Save();
            Load();
        }
    }
}

[Serializable]
class LoadData
{
    public int maxLevel;

    public void Save(int level)
    {
        maxLevel = level;
    }
}