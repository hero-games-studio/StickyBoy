using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    [SerializeField] float offset = -0.8f;
    [SerializeField] Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset);
    }

    public void startFollow()
    {
        StartCoroutine("updatePosition");
    }

    public void stopFollow()
    {
        StopCoroutine("updatePosition");
    }


    IEnumerator updatePosition()
    {
        while (true)
        {
            tempPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            transform.position = new Vector3(tempPos.x, tempPos.y, offset);
            yield return null;
        }
    }

}
