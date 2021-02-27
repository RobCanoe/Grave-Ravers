using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
   
   
    

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != null)
        {
            if (Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.V))
            {
                boxHolder.DetachChildren();

            }
            

            

        }
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if(grabCheck.collider !=null  && grabCheck.collider.tag == "Box")
        {
            if (Input.GetKey(KeyCode.G))
            {
                grabCheck.collider.gameObject.transform.parent = boxHolder;
                grabCheck.collider.gameObject.transform.position = boxHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            }else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }

        }
        


    }
}
