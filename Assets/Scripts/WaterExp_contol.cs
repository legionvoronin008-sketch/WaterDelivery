using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterExp_contol : MonoBehaviour
{
   public float damage;
    public float time;

     void Awake()
    {
        StartCoroutine(timer());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Explosion hit: " + collision.name);
        Health_Person enemy = collision.GetComponent<Health_Person>();
        Debug.Log("Health found: " + (enemy != null));
        //StartCoroutine(timer());
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

        }
    } 
    private IEnumerator timer()
	{

		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
