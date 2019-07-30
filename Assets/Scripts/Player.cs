using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GameAnalyticsSDK;


public class Player : MonoBehaviour
{

    public GameObject WinParticle; 
    public int NumberOfGrab;
    public TextMeshProUGUI NumberOfGrab_Text;
    bool DidaOneGarp=true;
    public GameObject[] Paint;
    public GameObject GrabParticle;
    PlayerLevelReached playerLevelReached;

    void Start()
    {
        
        playerLevelReached = GameObject.Find("LevelReachedManager").GetComponent<PlayerLevelReached>();
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version,LevelNumber());
        NumberOfGrab_Text.text = (NumberOfGrab.ToString()); // Convert from int to string;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag== "Finish")
        {
            Instantiate(WinParticle , transform.position , Quaternion.identity);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version,LevelNumber());
            Invoke("ResetGame", 3);
            playerLevelReached.PlayerLevel();
        }
    }
    private void ResetGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 20 )
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public int LevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void DidGrapObject()
    {
        if (DidaOneGarp&&NumberOfGrab>0)
        {
            NumberOfGrab--;
            NumberOfGrab_Text.text = (NumberOfGrab.ToString()); // Convert from int to string;
            DidaOneGarp = false;
            Invoke("CanGrap", 1);
        }
    }
    public void GrabEffect()
    {
        int RandomPaint = Random.Range(0, Paint.Length);
        Instantiate(Paint[RandomPaint], transform.position, Quaternion.identity);
        Instantiate(GrabParticle, transform.position, Quaternion.identity);
    }
    void CanGrap()
    {
        DidaOneGarp = true;
    }
}
