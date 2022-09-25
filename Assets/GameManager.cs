using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int kills = 0;

    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}
