using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{
    [SerializeField] bool isBig = false;
    Collider2D coll;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }
    public void onJump()
    {
        rb.simulated = false;
        coll.enabled = false;
        Invoke("resetStickedObject", 0.15f);
        if (!isBig)
        {
        }
    }
    private void resetStickedObject()
    {
        rb.simulated = true;
        coll.enabled = true;
    }
}
