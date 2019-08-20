using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
	[SerializeField] private int currentScore = 0;
	[SerializeField] private int highScore = 0;

	void Awake()
	{
		int numberOfSingletons = FindObjectsOfType<score>().Length;

		if (numberOfSingletons > 1)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void addToScore(int increment)
	{
		currentScore += increment;

		if (currentScore > highScore)
		{
			highScore = currentScore;
		}
	}

	public void resetScore()
	{
		currentScore = 0;
	}

	public string getCurrentScore()
	{
		return currentScore.ToString();
	}

	public string getHighScore()
	{
		return highScore.ToString();
	}
}
