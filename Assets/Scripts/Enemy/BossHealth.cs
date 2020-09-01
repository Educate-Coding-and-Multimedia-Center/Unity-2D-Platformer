using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	private Animator anim;
	private int health = 1;

	private bool canDamage;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		canDamage = true;
	}
	
	IEnumerator WaitForDamage()
	{
		yield return new WaitForSeconds(2f);
		canDamage = true;
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (canDamage)
		{
			if (target.tag == "Bullet")
			{
				health--;
				canDamage = false;

				if (health == 0)
				{
					anim.Play("BossDead");
					GetComponent<BoxCollider2D>().isTrigger = true;
					GetComponent<BossScript>().DeactivateBossScript();
				}

				StartCoroutine(WaitForDamage());
			}
		}
		
	}
}
