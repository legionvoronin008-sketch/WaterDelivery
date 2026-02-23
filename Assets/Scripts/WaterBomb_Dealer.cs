using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb_Dealer : MonoBehaviour
{
    public GameObject WaterExplosion;
    public GameObject body;
    public float time;
    //public Animator anim;
    void Awake()
    {
        transform.Rotate(0,0,90);
        StartCoroutine(timer());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground") || collision.CompareTag("Person"))
        {
            Instantiate(WaterExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private IEnumerator timer()
	{

		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
