using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour {

	public float moveSpeed = 5f;
	public Transform bottomCollision, topCollision, leftCollision, rightCollision;
	public LayerMask playerLayer;
	
	private Vector3 leftCollisionPosition, RightCollisionPosition;

	private Rigidbody2D body;
	private Animator anim;
	private bool moveLeft;
	private bool canMove;
	private bool stunned;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Start () {
		moveLeft = true;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove)
		{
			if (moveLeft)
			{
				body.velocity = new Vector2(-moveSpeed, body.velocity.y);
			}
			else
			{
				body.velocity = new Vector2(moveSpeed, body.velocity.y);
			}
		}
		CheckCollision();
	}

	void CheckCollision()
	{
		RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
		RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);

		Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

		if (topHit != null)
		{
			if (topHit.gameObject.tag == "Player")
			{
				if (!stunned)
				{
					topHit.gameObject.GetComponent<PlayerMovement>().OnStomping(7f);

					canMove = false;
					body.velocity = new Vector2(0, 0);

					stunned = true;

					// BEETLE CODE HERE
					if (gameObject.tag == "Beetle")
					{
						anim.Play("BeetleStunned");
						StartCoroutine(Dead(0.5f));
					}
					else
					{
						anim.Play("SnailStunned");
					}
				}
			}
		}

		if (leftHit)
		{
			if (leftHit.collider.gameObject.tag == "Player")
			{
				if (!stunned)
				{
					// Damage Player
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().DealDamage();
				}
				else
				{
					if (gameObject.tag != "Beetle")
					{
						body.velocity = new Vector2(15f, body.velocity.y);
						StartCoroutine(Dead(3f));
						print("Snail hit from left");
					}
				}
			}
		}

		if (rightHit)
		{
			if (rightHit.collider.gameObject.tag == "Player")
			{
				if (!stunned)
				{
					// Damage Player
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>().DealDamage();
				}
				else
				{
					if (gameObject.tag != "Beetle")
					{
						body.velocity = new Vector2(-15f, body.velocity.y);
						StartCoroutine(Dead(3f));
						print("Snail hit from right");
					}
				}
			}
		}

		// If not touching any ground
		if (!Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f))
		{
			ChangeDirection();
		}
	}

	void ChangeDirection()
	{
		moveLeft = !moveLeft;

		Vector3 tempScale = transform.localScale;

		if (moveLeft)
		{
			leftCollisionPosition = leftCollision.position;
			RightCollisionPosition = rightCollision.position;
		}
		else
		{
			leftCollisionPosition = rightCollision.position; 
			RightCollisionPosition = leftCollision.position;
		}
		tempScale.x = -tempScale.x;
		transform.localScale = tempScale;
	}

	IEnumerator Dead(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Bullet")
		{
			if (tag == "Beetle")
			{
				anim.Play("BeetleStunned");
				canMove = false;
				body.velocity = new Vector2(0, 0);
				StartCoroutine(Dead(0.4f));
			}

			if (tag == "Snail")
			{
				if (!stunned)
				{
					anim.Play("SnailStunned");
					canMove = false;
					stunned = true;
					body.velocity = new Vector2(0, 0);
					
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawLine(
			leftCollision.position, 
			new Vector3(leftCollision.position.x - 0.1f, leftCollision.position.y, leftCollision.position.z)
		);
		Gizmos.DrawLine(
			rightCollision.position,
			new Vector3(rightCollision.position.x + 0.1f, rightCollision.position.y, rightCollision.position.z)
		);
		Gizmos.DrawLine(
			bottomCollision.position,
			new Vector3(bottomCollision.position.x, bottomCollision.position.y - 0.1f, bottomCollision.position.z)
		);
		Gizmos.DrawSphere(
			topCollision.position,
			0.2f
		);
	}
}
