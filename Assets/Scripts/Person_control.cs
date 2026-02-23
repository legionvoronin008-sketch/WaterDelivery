using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_control : MonoBehaviour
{
    public float speed;
   public float damage;
   private Rigidbody2D rb;
   public float Speed_atack;
   private float targetSpeed;
   public float pushforce;
   private bool BorderTouch = false;
   private bool isTurning = false;
   public float triggerX = 10f;
   public float triggerY = 2f;
   public float ActionSt = 2f;
   public float ActionStY = 2f;
   public float JumpForce = 5f;
   public float JumpCooldown = 2f;
   public float jumpHeightTrigger = 2.5f;
   private bool Isjumpng = false;
   private bool canjump = true;


   public Animator anim;
   public SpriteRenderer sp;
   public Transform Playerposition;
   public Transform Banditposition;
   private int Playerflag = 1;
   private bool _IsChaised = false;
   private bool _IsAttacked = false;
   private bool isGrounded = true;
   private bool IsStunned = false;
   public float time;
   public GameObject Attack1;
   public GameObject Attack2;

   private float currentState , currentTimeToReverse;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStunned == true) 
        {
             Stun();
        }

        Vector2 posPX = new Vector2(Playerposition.position.x,0);
        Vector2 posPY = new Vector2(0,Playerposition.position.y);
        Vector2 posBX = new Vector2(Banditposition.position.x,0);
        Vector2 posBY = new Vector2(0,Banditposition.position.y);
        float distanceX = Vector2.Distance(posPX, posBX);
        float distanceY = Vector2.Distance(posPY, posBY);
        if (distanceX < triggerX && distanceY < triggerY)
        {
            Playerflag = 0;
        }
        else 
        {
            Playerflag = 1;
        }
        if (Playerflag == 1)
        {
            _IsChaised = false;
            if (isGrounded == true)
            {
                Idle();
            }
        }
        else
        {
            ChaisePlayer();
        }

    }
    void FixedUpdate()
    {
        if (IsStunned == true) 
        {
            Stun();
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetSpeed, 0.2f),rb.velocity.y);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Isjumpng = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border") && !isTurning)
        {
            anim.SetBool("IsMoving", false);
            isTurning = true;
            BorderTouch = true;
            rb.velocity = Vector2.zero;
            //Debug.Log("Touch");
            StartCoroutine(timer1());
        }
        else if (collision.CompareTag("Water_bullet"))
        {
            IsStunned = true;
        }
        else if (collision.CompareTag("Jum_Border")) 
        {
            if (isGrounded == false || canjump == false || IsStunned == true) 
            {
                return;
            }
            StartCoroutine(JumpCor());

        }

       // if (collision.CompareTag("Player") && !_IsAttacking)
        //{
            //Health enemy = collision.GetComponent<Health>();
            //Rigidbody2D Playerrb = collision.attachedRigidbody;
            //StartCoroutine(timer(enemy, Playerrb));
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            BorderTouch = false;
        }
    }
    private void Idle()
    {

        if (BorderTouch || _IsChaised)
        {
            return;
        }
        anim.SetBool("IsMoving" , true);
        rb.velocity = Vector2.right * speed;
        
    }
    public void ChaisePlayer()
    {
        anim.SetBool("IsMoving" , true);
        FacePlayer();

        float distanceX = Mathf.Abs(Playerposition.position.x - transform.position.x);
        //float distanceY = Mathf.Abs(Playerposition.position.y - transform.position.y);
    //if (distanceX > ActionSt && distanceY > jumpHeightTrigger)
    //{
       //JumpToPlayer(distanceX);
    //}
    if (distanceX > ActionSt) 
    {
         
        float dir = Mathf.Sign(Playerposition.position.x - transform.position.x);
        targetSpeed = dir * Speed_atack;
        _IsChaised = false;
        anim.SetBool("IsMoving" , true);
    }
    else if (distanceX <= ActionSt)
    {
        if (IsStunned == true) 
        {
            Stun();
        }
        else
        {
            
            _IsChaised = true;
            targetSpeed = 0f;
            anim.SetBool("IsMoving" , false);
        }
        if (_IsAttacked == false)
        {
            
            StartCoroutine(timer());
        }
    }

   
    }
    public void Stun()
    {
        StartCoroutine(StunCoroutine(time));
    }
    private void FacePlayer()
    {
        if (Playerposition.position.x < transform.position.x)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }
    //private void JumpToPlayer(float distanceX)
    //{
        //if (isGrounded == false || canjump == false || IsStunned == true) 
        //{
           // return;
        //}
        
            //StartCoroutine(JumpCor());
            
        
    //}
    private IEnumerator StunCoroutine(float time)
    {
        IsStunned = true;
        targetSpeed = 0f;
        //rb.velocity = Vector2.zero;
        anim.SetBool("IsMoving" , false);
        yield return new WaitForSeconds(time);
        IsStunned = false;
    }
    private IEnumerator timer1()
    {
        yield return new WaitForSeconds(1f);
        sp.flipX = !sp.flipX;
        isTurning = false;
        BorderTouch = false;
        speed *= -1;
        float dir = sp.flipX ? -1f : 1f;
        rb.velocity = Vector2.right * dir * speed;


    }
    private IEnumerator timer()
    {
        _IsAttacked = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        Attack1.SetActive(true);
        Attack2.SetActive(true);
	    yield return new WaitForSeconds(0.1f);
	    Attack1.SetActive(false);
        Attack2.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _IsAttacked = false;
    }
    private IEnumerator JumpCor()
    {
        canjump = false;
        Isjumpng = true;
        rb.velocity = new Vector2 (rb.velocity.x, 0f);
        rb.AddForce(Vector2.up*JumpForce, ForceMode2D.Impulse);
        yield return new WaitUntil(() => isGrounded);
        Isjumpng  = false;
        yield return new WaitForSeconds(JumpCooldown);
        canjump = true;
    }
}
