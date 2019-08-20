using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleton : MonoBehaviour
{
	void Awake()
	{
		int numberOfSingletons = FindObjectsOfType<singleton>().Length;

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
	
}
