using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject targetPoint;
    [SerializeField] GameObject mousePoint;
    [SerializeField] GameObject forceArrow;
    [SerializeField] GameObject forceCircle;

    private float currentDistance;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float safeSpace;
    [SerializeField] float shootPower;


    [SerializeField] float thrust = 10f;
    private Vector3 shootDirection;

    [SerializeField] float offset = -0.8f;

    [SerializeField] List<GameObject> stickyPoints = new List<GameObject>();

    private Vector3 firstPos;

    [SerializeField] float radius = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("UpdateRoutine");
    }


    IEnumerator mouseDrag()
    {
        while (true)
        {
            currentDistance = Vector3.Distance(mousePoint.transform.position, firstPos);

            if (currentDistance > radius)
            {
                Vector3 fromOriginToObject = mousePoint.transform.position - firstPos; //~GreenPosition~ - *BlackCenter*
                fromOriginToObject *= radius / currentDistance; //Multiply by radius //Divide by Distance
                mousePoint.transform.position = firstPos + fromOriginToObject;
            }

            if (currentDistance <= maxDistance)
            {
                safeSpace = currentDistance;
            }
            else
            {
                safeSpace = maxDistance;
            }


            shootPower = Mathf.Abs(safeSpace) * thrust;

            Vector3 dimXY = mousePoint.transform.position - firstPos;
            float diffrence = dimXY.magnitude;


            targetPoint.transform.position = transform.position - dimXY;
            targetPoint.transform.position = new Vector3(targetPoint.transform.position.x, targetPoint.transform.position.y, offset);


            shootDirection = Vector3.Normalize(transform.position - targetPoint.transform.position);

            yield return null;
        }
    }


    IEnumerator UpdateRoutine()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePoint.GetComponent<MouseFollow>().startFollow();
                firstPos = mousePoint.transform.position;
                StartCoroutine("mouseDrag");
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (GameObject st in stickyPoints)
                {
                    st.GetComponent<Sticky>().setStickedObjectAsTriggered();
                }

                Vector3 push = shootDirection * shootPower * -1;
                GetComponent<Rigidbody2D>().AddForce(push, ForceMode2D.Impulse);


                targetPoint.transform.position = new Vector3(transform.position.x, transform.position.y, offset);
                mousePoint.GetComponent<MouseFollow>().stopFollow();
                mousePoint.transform.position = new Vector3(transform.position.x, transform.position.y, offset);

                StopCoroutine("mouseDrag");
            }

            yield return null;
        }
    }
}
