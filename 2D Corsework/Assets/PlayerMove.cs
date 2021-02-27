using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public delegate void DeadEventHandler();

public class PlayerMove : Character
{

    private static PlayerMove instance;





    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask WhatIsGround;
    private bool immortal = false;
    [SerializeField]
    private float immortalTime;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private GameObject ArrowPrefab;
    public Rigidbody2D MyRigibody { get; set; }
    public event DeadEventHandler Dead;
    private Vector2 startPos;
    private int Coin = 0;
    bool isMoving = false;
  

    private AudioSource audioSrc;
    
    public GameChecker gameChecker;
    



    public bool Slide { get; set; }

    public bool Jump { get; set; }
    public bool OnGround { get; set; }
   
    public static PlayerMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerMove>();
            }
            return instance;
        }

    }
    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {


                OnDead();
            }
            return healthStat.CurrentVal <= 0;
        }
    }
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigibody = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();


    }
    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    void Update()
    {
        if (!TakingDamage && !IsDead)
        {


            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
        if (Coin == 100)
        {
            //Loads the scene
            SceneManager.LoadScene("NameOfYourScene", LoadSceneMode.Single);
        }
        if (MyRigibody.velocity.x != 0)
        {
            isMoving = true;

        }else
            isMoving = false;
        if (isMoving)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }

        }
        else
            audioSrc.Stop();
    }
    public void OnDead()
    {
        if (Dead!=null)
        {

            Dead();
        }
    }
    void FixedUpdate()
    {
        if (!TakingDamage)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            HandleMovement(horizontal);


            HandleLayers();
            Flip(horizontal);
           
        }

    }
    private void HandleMovement(float horizontal)
    {
        if(MyRigibody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if(!LightAttack && !Slide || (OnGround || airControl))
        {
            MyRigibody.velocity = new Vector2(horizontal * movementSpeed, MyRigibody.velocity.y);
          

        }
        if (Jump && MyRigibody.velocity.y ==0)
        {
            MyRigibody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
   
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
            soundManagerScript.PlaySound("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MyAnimator.SetTrigger("attack");
            soundManagerScript.PlaySound("swordSwishing");
        }
        if (Input.GetKeyDown("c"))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("throw");
            soundManagerScript.PlaySound("fire");
        }
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
 
    private bool IsGrounded()
    {
        if (MyRigibody.velocity.y <=0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, WhatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                        
                    }
                }
            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }
    public void FireArrow(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            

            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
                tmp.GetComponent<Arrow>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(ArrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<Arrow>().Initialize(Vector2.left);
            }
        }
    }
    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 50;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("Damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
                soundManagerScript.PlaySound("playerHit");


            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("Die");
                FindObjectOfType<GameChecker>().EndGame();
                soundManagerScript.PlaySound("playerHit");

            }
        }
       
    }
    public override IEnumerator SuperDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 0;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("Damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
                soundManagerScript.PlaySound("playerHit");


            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("Die");
                FindObjectOfType<GameChecker>().EndGame();
                soundManagerScript.PlaySound("playerHit");

            }
        }

    }
    public override void Death()
    {
        MyRigibody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrentVal = healthStat.MaxVal;
        transform.position = startPos;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            soundManagerScript.PlaySound("pick_up");
            GameManager.Instance.CollectedCoins++;
            Destroy(other.gameObject);
            Coin++;
            
            if (Coin >= 30)
            {
                ArrowPrefab.tag = "Arrow";
                gameChecker.CompleteLevel();
            }
        }
        if (other.gameObject.tag == "Heart")
        {
            soundManagerScript.PlaySound("pick_up");
            Destroy(other.gameObject);
            healthStat.CurrentVal += 50;
        }
        if (other.gameObject.tag == "BraveHeart")
        {
            soundManagerScript.PlaySound("pick_up");
            Destroy(other.gameObject);
            healthStat.CurrentVal += 100;

            
            ArrowPrefab.tag = "SuperWeapon";
            
            
            


        }
    }
   
}
    

