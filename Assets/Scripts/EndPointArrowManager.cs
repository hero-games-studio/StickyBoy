using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointArrowManager : MonoBehaviour
{
    public Vector2 EndPoint;
    public float Z_angle;
    public RectTransform transformX;
    public float X_postion;
    // Start is called before the first frame update
    void Start()
    {
        EndPoint = GameObject.FindGameObjectWithTag("Finish").transform.position; // get postion of end point;
        transformX = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Z_angle = transform.rotation.eulerAngles.z;
        X_postion = transformX.position.x;
        Vector2 direction = new Vector2( EndPoint.x - transform.position.x, EndPoint.y - transform.position.y); // Look At;
        this.transform.up = direction;
        /*
        if (Z_angle > 44 && Z_angle < 133)
        {
            if(Z_angle < 90)
            {
                transform.position = new Vector2(Z_angle+10, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(Z_angle - 10, transform.position.y);
            }
        }
        else
        {
        }
        */
    }

}
