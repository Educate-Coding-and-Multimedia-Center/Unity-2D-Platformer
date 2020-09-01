using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour {

	private AudioSource coinSound;
	public Text scoreText;

	//private int score = 0;

	void Start()
	{
		coinSound = GetComponent<AudioSource>();
		scoreText.text = "x" + LevelController.scores;
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Coin")
		{
			GetCoin();

			target.gameObject.SetActive(false);
		}
	}

	public void GetCoin()
	{
		LevelController.scores++;

		scoreText.text = "x" + LevelController.scores;

		coinSound.Play();

	}
}
