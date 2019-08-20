using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleLasers : MonoBehaviour
{
	[SerializeField] AudioClip sound;
	[SerializeField] float volume;
	[SerializeField] float powerupDuration = 10f;

	private void OnTriggerEnter2D(Collider2D otherThing)
	{
		if (otherThing.tag != "objectDestroyer")
		{
			otherThing.gameObject.GetComponent<Player>().dl(powerupDuration);
			AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
		}
		Destroy(gameObject);
	}
}
