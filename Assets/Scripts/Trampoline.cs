using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
	public float pushforce = 15;
	public float DBKoof = 2;
	private bool Doudblejump = false;
	private bool Timerrunning = false;
	public float timerDJ = 2.5f;
	public Animator animator;
	public int FlagDirection = 0;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && Doudblejump == false)
		{
			Rigidbody2D Playerrb = collision.attachedRigidbody;
			if (FlagDirection == 0)
			{
				Jumper(Playerrb);
			}
			else
			{
				JumperDir(Playerrb);
			}
			//Doudblejump = true;
			//Debug.Log("1jump");
			//Debug.Log(Doudblejump);
			animator.SetTrigger("Interact");
			Doudblejump = true;
			if (Timerrunning == false)
			{
				StartCoroutine(timerDBjump());
			}
		}
		//else if (collision.CompareTag("Player") && Doudblejump == true)
		//{
			//Rigidbody2D Playerrb = collision.attachedRigidbody;
			//Debug.Log("2jump");
			//animator.SetTrigger("Interact");
			//DbJumper(Playerrb);
			//Doudblejump = false;
		//}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (Doudblejump == true)
		{
			Rigidbody2D Playerrb = collision.attachedRigidbody;
			DbJumper(Playerrb);
			animator.SetTrigger("Interact");
			Doudblejump = false;
		}
	}
	public void Jumper(Rigidbody2D Playerrb)
	{
		Playerrb.velocity = Vector2.zero;
		Vector2 knockback = Vector2.up * pushforce;
		Playerrb.AddForce(knockback, ForceMode2D.Impulse);
	}
	public void JumperDir(Rigidbody2D Playerrb)
	{
		Playerrb.velocity = Vector2.zero;
		Vector2 knockback = Vector2.right * pushforce;
		Playerrb.AddForce(knockback, ForceMode2D.Impulse);
	}
	public void DbJumper(Rigidbody2D Playerrb)
	{
		Playerrb.velocity = Vector2.zero;
		Vector2 dbknockback = Vector2.up * pushforce * DBKoof;
		Playerrb.AddForce(dbknockback, ForceMode2D.Impulse);
	}
	private IEnumerator timerDBjump()
	{
		Timerrunning = true;
		yield return new WaitForSeconds(timerDJ);
		Doudblejump = false;
		Timerrunning = false;
	}
}
