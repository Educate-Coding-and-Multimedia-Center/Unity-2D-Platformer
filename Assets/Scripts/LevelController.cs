using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour {

	public static int playerLives = 5;
	public static int scores = 0;
	public static int bullets = 3;

	public string nextLevel;

	public void NextLevel()
	{
		SceneManager.LoadScene(nextLevel);
	}

}
