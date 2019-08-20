using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
	[SerializeField] int healthToAdd = 300;
	[SerializeField] AudioClip sound;
	[SerializeField] float volume = 9f;
	private void OnTriggerEnter2D(Collider2D otherThing)
	{
		if (otherThing.tag != "objectDestroyer")
		{
			otherThing.gameObject.GetComponent<Player>().addToHealth(healthToAdd);
			AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
		}

		Destroy(gameObject);
	}
}
