using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 5f;
    public float Time1 = 20.0f;
    public float Time2 = 20.0f;
    public float Time3 = 23.0f;
    public float Pause = 24.0f;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool Moveflag = false;
    public Animator animator;
    public GameObject Button;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2( speed, 0);
        animator = GetComponent<Animator>();
        StartCoroutine(Dialog());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Moveflag == true)
        {
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private IEnumerator Dialog()
    {
         yield return new WaitForSeconds(Time1);
         Moveflag  = true;
         animator.SetBool("Walk",true);
         yield return new WaitForSeconds(Time2);
         Moveflag  = false;
         animator.SetBool("Walk",false);
         yield return new WaitForSeconds(Pause);
         Moveflag  = true;
         animator.SetBool("Walk",true);
         yield return new WaitForSeconds(Time3);
         Moveflag  = false;
         animator.SetBool("Walk",false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn1"))
        {
            Button.SetActive(true);
        }
    }
}
