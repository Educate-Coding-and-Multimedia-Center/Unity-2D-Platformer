using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour {

	private Rigidbody2D body;
	private Animator anim;

	private Vector3 moveDirection = Vector3.down;

	private Coroutine movementCoroutine;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		movementCoroutine = StartCoroutine(ChangeMovement());
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(moveDirection * Time.deltaTime);
	}

	IEnumerator ChangeMovement()
	{
		yield return new WaitForSeconds(Random.Range(2f, 5f));

		if (moveDirection == Vector3.down)
		{
			moveDirection = Vector3.up;
		}
		else
		{
			moveDirection = Vector3.down;
		}

		movementCoroutine = StartCoroutine(ChangeMovement());
	}

	IEnumerator SpiderDead()
	{
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Bullet")
		{
			anim.Play("SpiderDead");
			body.bodyType = RigidbodyType2D.Dynamic;
			StartCoroutine(SpiderDead());
			StopCoroutine(movementCoroutine);
		}
	}
}
