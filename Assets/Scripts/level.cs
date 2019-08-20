using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level : MonoBehaviour
{
	public void LoadGameScene()
	{
		SceneManager.LoadScene("Game");
	}

	public void resetScore()
	{
		score score = FindObjectOfType<score>();
		score.resetScore();
	}
}
