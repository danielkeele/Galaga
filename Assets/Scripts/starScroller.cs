using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starScroller : MonoBehaviour
{	
	[SerializeField] float scrollSpeed = .5f;
	[SerializeField] float scrollFactor = .5f;
	[SerializeField] public bool respondToInput = true;
	private ParticleSystem ps;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();
	}
	void Update()
	{
			float playerXMovement = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
			playerXMovement /= (scrollFactor * 3.6f);
			var main = ps.main;
			float newXLocation = Mathf.Clamp(transform.position.x + (-1 * (Input.GetAxis("Horizontal")) / (scrollFactor * 25)),-94f,94f);

			if (respondToInput)
			{
			transform.position = new Vector3(newXLocation, transform.position.y, transform.position.z);
			main.simulationSpeed = scrollSpeed + playerXMovement;
			}
			else
			{
				main.simulationSpeed = scrollSpeed;
			}
	}
}
