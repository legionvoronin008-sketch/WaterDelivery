using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public float maxhealth;
	private float currentH;
	private bool isAlive;
	public  GameObject body;
	//public Text CurrentHPtext;
	public GameObject Dead_pannel;
	public Animator anim;
	public Image img;
	public GameObject DamagePT;
	private void Awake()
	{
		currentH =  maxhealth;
		isAlive = true;
		Time.timeScale = 1f;
		img.fillAmount = 1f;
	}
	public void TakeDamage(float damage)
	{
		if (isAlive == true && body.CompareTag("Player"))
		{
			currentH -= damage;
			img.fillAmount = currentH * 0.01f;
			Instantiate(DamagePT ,transform.position, Quaternion.identity);
			StartCoroutine(timer1());
			ChechAlive();
		}
		if (isAlive == false && body.CompareTag("Player")) 
		{
			Constants.DeathCount++;
			Dead_pannel.SetActive(true);
			anim.SetBool("Dead",true);
			Debug.Log("Player is dead" + Constants.DeathCount);
			Instantiate(DamagePT ,transform.position, Quaternion.identity);
			Instantiate(DamagePT ,transform.position, Quaternion.identity);
			Instantiate(DamagePT ,transform.position, Quaternion.identity);
			StartCoroutine(timer());
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
		}
	}
	private IEnumerator timer()
	{
		yield return new WaitForSeconds(1f);
		Time.timeScale = 0f;
	}
	private IEnumerator timer1()
	{
		anim.SetTrigger("GetHurt");
		yield return new WaitForSeconds(1f);
		anim.SetTrigger("AnimationBack");
	}
	//private IEnumerator timer2()
	//{
		//anim.SetTrigger("GetHurt");
		//yield return new WaitForSeconds(1.5f);
		//anim.SetBool("Isdead",true);
	//}
}
