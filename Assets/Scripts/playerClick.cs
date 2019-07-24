using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClick : MonoBehaviour
{

    [SerializeField] float diss = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("followPlayer");
    }

    IEnumerator followPlayer()
    {
        while (true)
        {
            transform.position = new Vector3(PlayerManager.Instance.player.transform.position.x, PlayerManager.Instance.player.transform.position.y, diss);
            yield return null;
        }
    }


    private void OnMouseDrag()
    {
        PlayerManager.Instance.player.GetComponent<trajectoryScript>().readyToJump();
    }

    private void OnMouseUp()
    {
        PlayerManager.Instance.player.GetComponent<trajectoryScript>().jumpNow();
    }
}
