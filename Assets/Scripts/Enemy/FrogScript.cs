using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {

	private bool jumpLeft = true;
	private bool animationStart;
	private bool animationFinished;
	private int jumpedTimes;

	private Animator anim;
	private Coroutine coroutine;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		coroutine = StartCoroutine(FrogJump());
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (animationStart && animationFinished)
		{
			animationStart = false;
			transform.parent.position = transform.position;
			transform.localPosition = Vector2.zero;
		}
	}

	IEnumerator FrogJump()
	{
		yield return new WaitForSeconds(Random.Range(1f, 4f));
		animationStart = true;
		animationFinished = false;

		jumpedTimes++;

		if (jumpLeft)
		{
			anim.Play("FrogJumpLeft");
		}
		else
		{
			anim.Play("FrogJumpRight");
		}

		coroutine = StartCoroutine(FrogJump());
		print("jumping");
	}

	public void AnimationFinished()
	{
		animationFinished = true;

		if (jumpLeft)
		{
			anim.Play("FrogIdleLeft");
		}
		else
		{
			anim.Play("FrogIdleRight");
		}

		if (jumpedTimes == 3)
		{
			jumpedTimes = 0;
			Vector3 tempScale = transform.localScale;
			tempScale.x *= -1;
			transform.localScale = tempScale;

			jumpLeft = !jumpLeft;
		}
	}
}
