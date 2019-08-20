using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayScore : MonoBehaviour
{
	[SerializeField] GameObject currentTextObject;
	[SerializeField] GameObject highTextObject;
	Text currentText;
	Text highText;
	score score;

	void Start()
	{
		currentText = currentTextObject.GetComponent<Text>();
		highText = highTextObject.GetComponent<Text>();
		score = FindObjectOfType<score>();
	}

	void Update()
	{
		currentText.text = score.getCurrentScore();
		highText.text = score.getHighScore();
	}
}
