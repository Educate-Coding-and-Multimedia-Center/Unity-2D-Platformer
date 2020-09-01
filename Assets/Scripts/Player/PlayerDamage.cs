using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

	public Text liveText;
	public int playerLives = 5;

	private bool canDamage = true;
	//private int lives = 1;

	// Use this for initialization
	void Start () {
		liveText.text = "x" + LevelController.playerLives;

		Time.timeScale = 1f;
	}
	
	public void DealDamage()
	{
		if (canDamage)
		{
			LevelController.playerLives--;
			
			if (LevelController.playerLives >= 0)
			{
				liveText.text = "x" + LevelController.playerLives;
			}

			if (LevelController.playerLives == 0)
			{
				// Restart game
				Time.timeScale = 0f;
				StartCoroutine(RestartGame());
			}

			canDamage = false;

			StartCoroutine(WaitForDamage());
		}

	}

	IEnumerator WaitForDamage()
	{
		yield return new WaitForSeconds(2f);
		canDamage = true;
	}

	IEnumerator RestartGame()
	{
		yield return new WaitForSecondsRealtime(2f);
		LevelController.playerLives = playerLives;
		liveText.text = "x" + LevelController.playerLives;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "DeadZone")
		{
			StartCoroutine(RestartGame());
		}
	}
}
