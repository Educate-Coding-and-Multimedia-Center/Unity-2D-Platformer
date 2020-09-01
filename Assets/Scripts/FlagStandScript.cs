using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagStandScript : MonoBehaviour {

	public Transform flag;
	public AudioSource levelClearSound;
	public float flagMaxHeight = 3.25f;
	public float flagSpeed = 1f;

	private bool isFlagAtTop = false;
	private bool startMovingFlag = false;
	
	// Update is called once per frame
	void Update () {
		if (startMovingFlag)
		{
			flag.Translate(Vector2.up * flagSpeed * Time.deltaTime);
			if (flag.localPosition.y >= flagMaxHeight)
			{
				startMovingFlag = false;
				isFlagAtTop = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D target)
	{
		if (target.tag == "Player")
		{
			if (!isFlagAtTop)
			{
				startMovingFlag = true;
				levelClearSound.Play();
				target.gameObject.GetComponent<PlayerMovement>().SetWalkToCastle();
			}
		}
	}
}
