using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_dealer : MonoBehaviour
{
    public float damage;
    public GameObject body;
    public float time;
    public Animator anim;
    void Awake()
    {
        StartCoroutine(timer());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Person"))
        {
            Health_Person enemy = collision.GetComponent<Health_Person>();
            enemy.TakeDamage(damage);
            anim.SetBool("ISUsed",true);
            StartCoroutine(timer());
        }
    }
    private IEnumerator timer()
	{

		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
