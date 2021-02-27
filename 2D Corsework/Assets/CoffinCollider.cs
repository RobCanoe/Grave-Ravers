using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinCollider : MonoBehaviour
{
    private const float SPEED = 50f;
    // Start is called before the first frame update
    private void Update()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.UpArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        transform.position += moveDir * SPEED * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger!");
    }
}
