using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelReached : MonoBehaviour
{
    public int LevelReached;
    // Start is called before the first frame update

    [System.Obsolete]
    void Start()
    {
        DontDestroyOnLoad(this);
        LevelReached = PlayerPrefs.GetInt("PLRed", 1);
        Application.LoadLevel(LevelReached);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerLevel()
    {
        PlayerPrefs.SetInt("PLRed", SceneManager.GetActiveScene().buildIndex+1);
    }

}
