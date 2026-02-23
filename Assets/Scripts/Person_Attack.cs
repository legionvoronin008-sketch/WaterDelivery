using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_Attack : MonoBehaviour
{
    public float pushforce;
    public float damage;
    private bool playerflag = false;
    // Start is called before the first frame update
 private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerflag = true;
            Debug.Log("Entered");
        }
        if (playerflag == true)
        {
            Health enemy = collision.GetComponent<Health>();
            Rigidbody2D Enemyrb = collision.attachedRigidbody;
            GiveDamage(enemy, Enemyrb);
        }
    }
 private void OnTriggerExit2D(Collider2D collision)
 {
     if (collision.CompareTag("Player"))
     {
         playerflag = false;

     }
 }
 public void GiveDamage(Health enemy, Rigidbody2D Enemyrb)
 {
     enemy.TakeDamage(damage);
     Vector2 knockback = Vector2.right * pushforce; //Mathf.Sign(Enemyrb.transform.position.x - transform.position.x)
     Enemyrb.AddForce(knockback, ForceMode2D.Impulse);
 }
}
