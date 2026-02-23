using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Person : MonoBehaviour
{
    public float maxhealth;
	private float currentH;
	private bool isAlive;
	public  GameObject body;
	public  GameObject Controlbody;
	public  GameObject Head;
	public Animator anim;
	public  GameObject Attacking_space1;
	public  GameObject Attacking_space2;
	public Player_control player;
	public GameObject effect;
	private void Awake()
	{
		currentH =  maxhealth;
		isAlive = true;
		Time.timeScale = 1f;
	}
	public void TakeDamage(float damage)
	{
		if (isAlive == true && body.CompareTag("Person"))
		{
			currentH -= damage;
			ChechAlive();
		}
		if (isAlive == false && body.CompareTag("Person")) 
		{
			anim.SetBool("GotWater",true);
			Attacking_space1.SetActive(false);
			Attacking_space2.SetActive(false);
			Instantiate(effect ,transform.position, Quaternion.identity);
			Controlbody.layer = 6;
			Person_control person = Controlbody.GetComponent<Person_control>();
			person.enabled = false;
			Head.SetActive(false);
		}
	}
	private void ChechAlive()
	{
		if (currentH > 0)
		{
			isAlive = true;
		}
		else
		{
			isAlive = false;
			player.AddPerson();
		}
	}

}
