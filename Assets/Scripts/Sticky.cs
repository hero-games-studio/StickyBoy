 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public DistanceJoint2D stick;
    // [SerializeField] float breakForce = 600f;
    public Collision2D stickedObject = null;
    public bool sticked;
    GameObject Player;
    Player playerScript;
    RipplePostProcessor CamRipple;
    public LineRenderer line;
    public Material Mat;
    int CountLine;
    // public GameObject StickPartical;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<Player>();
        stick = gameObject.GetComponent<DistanceJoint2D>();
        CamRipple = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RipplePostProcessor>();
        //stick.breakForce = breakForce;
        stick.enabled = false;
        sticked = false;
    }
    void Update()
    {
        if (!sticked)
        {
            line.SetPosition(1, transform.position);
        }
        line.SetPosition(0, transform.position);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D Contact = other.contacts[0];
        if (other.gameObject.tag != "Ball")
        {
        }
        if (other.transform.CompareTag("StickAble"))
        {
            if (stickedObject == null)
            {
                playerScript.GrabEffect();
                CamRipple.RippleEffect();
                stickedObject = other;
                stick.connectedBody = stickedObject.gameObject.GetComponent<Rigidbody2D>();
                stick.enabled = true;
                sticked = true;
                letPlayerJump();
                releaseAdditionalStickedPoints();
                if (this.transform.position != other.transform.position)
                {
                    line.SetPosition(1, Contact.point);
                }
            }
        }
    }
    public void releaseJoint()
    {
        stick.enabled = false;
        sticked = false;
        stick.connectedBody = null;
        //stick = gameObject.AddComponent<DistanceJoint2D>();
    }
    internal void setStickedObjectAsTriggered()
    {
        if (stickedObject != null)
        {
            stickedObject.gameObject.GetComponent<StickyObject>().onJump();
            stickedObject = null;
            releaseJoint();
        }
    }
    private void letPlayerJump()
    {
        PlayerManager.Instance.player.GetComponent<trajectoryScript>().canJumpNow();
    }
    private void releaseAdditionalStickedPoints()
    {
        PlayerManager.Instance.player.GetComponent<trajectoryScript>().checkStickedPoints();
    }
    void CreateLine()
    {
        line = new GameObject("Line" + CountLine).GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.useWorldSpace = true;
        line.numCapVertices = 50;
    }
}
