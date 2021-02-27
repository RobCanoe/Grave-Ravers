using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    public Animator animator;


    float horizontalMove = 0f;
    bool crouch = false;

    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        Debug.Log(Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        
    }
    public void OnLinding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCtrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        
        jump = false;
       
    } 
}
