using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

	[SerializeField] float scrollSpeed = .1f;
	[SerializeField] public bool adjustingBackground = false;
	Material myMaterial;
	Vector2 offset;
	void Start()
	{
		myMaterial = GetComponent<Renderer>().material;
	}
	
	void Update()
	{
		if (adjustingBackground)
		{
		float playerXMovement = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
		playerXMovement /= 12;
		float playerYMovement = Input.GetAxis("Horizontal") / 10f;
		offset = new Vector2(playerYMovement, (scrollSpeed/10) + playerXMovement);
		}
		else
		{
			offset = new Vector2(0, scrollSpeed/10);
		}
		myMaterial.mainTextureOffset += offset * Time.deltaTime;
	}
}
