using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	public GameObject stonePrefab;
	public Transform stonePosition;

	private Animator anim;

	private string coroutine = "StartAttack";

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		StartCoroutine(coroutine);
	}


	void BackToIdle()
	{
		anim.Play("BossIdle");
	}

	void Attack()
	{
		GameObject stone = Instantiate(stonePrefab, stonePosition.position, Quaternion.identity);
		Vector2 force = new Vector2(Random.Range(-300f, -700f), 0f);
		stone.GetComponent<Rigidbody2D>().AddForce(force);
	}

	IEnumerator StartAttack()
	{
		yield return new WaitForSeconds(Random.Range(2f, 5f));

		anim.Play("BossAttack");

		StartCoroutine(coroutine);
	}

	public void DeactivateBossScript()
	{
		StopCoroutine(coroutine);
		enabled = false;
	}
}
