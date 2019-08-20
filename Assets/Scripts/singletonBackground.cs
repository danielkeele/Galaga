using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class singletonBackground : MonoBehaviour
{
	starScroller[] ss;
	BackgroundScroller bs;

	void Awake()
	{
		int numberOfBackgrounds = FindObjectsOfType<singletonBackground>().Length;

		if (numberOfBackgrounds > 1)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		ss = FindObjectsOfType<starScroller>();
		bs = FindObjectOfType<BackgroundScroller>();
	}

	void setRespondToInput(bool tf)
	{
		for (int x = 0; x < ss.Length; x++)
		{
			ss[x].respondToInput = tf;
		}
		bs.adjustingBackground = tf;
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name == "Game" && FindObjectsOfType<Player>().Length == 1)
		{
			setRespondToInput(true);	
		}
		else if (SceneManager.GetActiveScene().name == "StartMenu")
		{
			setRespondToInput(false);
		}
		else if (SceneManager.GetActiveScene().name == "GameOver")
		{
			setRespondToInput(false);
		}
		else
		{
			setRespondToInput(false);
		}
	}
}
