using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour {

	public Transform bottomCollision;
	public LayerMask playerLayer;

	private Animator anim;
	private bool canAnimate;
	private bool startAnimate;
	private Vector3 moveDir = Vector3.up;
	private Vector3 originPosition;
	private Vector3 animPosition;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		originPosition = transform.position;
		animPosition = new Vector3(transform.position.x, transform.position.y + 0.15f);

		canAnimate = true;
	}
	
	// Update is called once per frame
	void Update () {
		CheckCollision();
		AnimateBlock();
	}

	void CheckCollision()
	{
		if (canAnimate)
		{
			RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector3.down, 0.1f, playerLayer);

			if (hit)
			{
				startAnimate = true;
				canAnimate = false;


				FindObjectOfType<ScoreScript>().GetCoin();
			}
		}
	}

	void AnimateBlock()
	{
		if (startAnimate)
		{
	
			transform.Translate(moveDir * Time.smoothDeltaTime);

			if (transform.position.y > animPosition.y)
			{
				moveDir = Vector3.down;
			}
			if (transform.position.y < originPosition.y)
			{
				startAnimate = false;

				anim.Play("BonusBlockIdle");
			}
		}
	}
}
