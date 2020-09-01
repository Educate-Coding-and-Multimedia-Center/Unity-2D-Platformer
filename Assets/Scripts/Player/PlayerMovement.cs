using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;
	public float jumpPower = 10f;
	public Transform groundCheckPosition;
	public LayerMask groundLayer;

	private Rigidbody2D body;
	private Animator anim;
	private bool isGrounded;
	private bool jumped;

	private bool walkToCastle = false;

	void Awake()
    {
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfGrounded();
		PlayerJump();
		WalkToCastle();
	}

	void FixedUpdate ()
	{
		PlayerWalk();
	}

	void PlayerWalk()
	{
		if (walkToCastle)
			return;

		float horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput > 0)
		{
			body.velocity = new Vector2(speed, body.velocity.y);

			ChangeDirection(1);
		}
		else if (horizontalInput < 0)
		{
			body.velocity = new Vector2(-speed, body.velocity.y);

			ChangeDirection(-1);
		}
		else
		{
			body.velocity = new Vector2(0f, body.velocity.y);
		}

		anim.SetFloat("speed", Mathf.Abs(body.velocity.x));
	}

	void CheckIfGrounded()
	{
		isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

		if (isGrounded)
		{
			if (jumped)
			{
				jumped = false;

				anim.SetBool("jump", false);
			}
		}
	}

	void PlayerJump()
	{
		if (walkToCastle)
			return;

		if (isGrounded)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				jumped = true;
				body.velocity = new Vector2(body.velocity.x, jumpPower);

				anim.SetBool("jump", true);
			}
		}
	}

	void ChangeDirection(int direction)
	{
		Vector3 tempScale = transform.localScale;
		tempScale.x = direction;
		transform.localScale = tempScale;
	}

	public void OnStomping(float jumpForce)
    {
		body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

	public void SetWalkToCastle()
	{
		if (!walkToCastle)
		{
			walkToCastle = true;
		}
	}

	void WalkToCastle()
	{
		if (walkToCastle)
		{
			anim.SetFloat("speed", 1f);
			transform.Translate(Vector2.right * 1f * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Goal")
		{
			FindObjectOfType<LevelController>().NextLevel();
		}
	}
}
