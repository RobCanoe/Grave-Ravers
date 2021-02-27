using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{

    [SerializeField]
    public GameObject gameObject;

    [SerializeField]
    protected float movementSpeed;
   
    [SerializeField]
    protected Stats healthStat;
   

    protected bool facingRight;
    public bool LightAttack { get; set; }
    public Animator MyAnimator { get; private set; }
    // Start is called before the first frame update
    [SerializeField]
    private List<string> damageSources;
    [SerializeField]
    private EdgeCollider2D swordCollider;
    public abstract bool IsDead { get; }
    public bool TakingDamage { get; set; }
    
    
    public virtual void Start()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
        
        healthStat.Initalize();
    }
    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeDirection()
    {
        facingRight = !facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    public abstract IEnumerator TakeDamage();
    public abstract IEnumerator SuperDamage();

    public abstract void Death();
    
    public void MeleeAttack()
    {
        SwordCollider.enabled = true;
    }
   
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DamageCharacter");
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
      
    }
   
}
