using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameInstructions : MonoBehaviour
{	
	[Header("Startup Items")]
	[SerializeField] GameObject spawner;
	[SerializeField] GameObject player;
	[SerializeField] GameObject healthIndicatorBar;
	[SerializeField] GameObject healthIndicatorText;


	void Update()
	{
		bool alive = FindObjectsOfType<Player>().Length == 1;

		if (!alive)
		{
			StartCoroutine(gameOver());
		}
	}

	IEnumerator gameOver()
	{
		spawner.SetActive(false);
		yield return new WaitForSeconds(5.5f);
		SceneManager.LoadScene("GameOver");
	}
}
