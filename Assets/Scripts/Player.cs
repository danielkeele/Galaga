using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	[Header("Player")]
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float padding = 0.5f;
	[SerializeField] int health = 200;
	[SerializeField] GameObject healthText;
	int originalHealth;
	[SerializeField] GameObject healthIndicator;

	[Header("Laser Config")]
	[SerializeField] float laserSpeed = 10f;
	[SerializeField] float timeBetweenLasers = .5f;
	[SerializeField] GameObject laserPrefab;
	[SerializeField] float numLasers = 1;
	float doubleLasersTimer = 6f;

	[Header("Sound")]

	[SerializeField] AudioClip laserSound;
	[SerializeField] float laserVolume = 50;
	[SerializeField] AudioClip explosionSound;
	[SerializeField] float explodeVolume = 75f;
	[SerializeField] AudioClip takeDamageSound;
	[SerializeField] float takeDamageVolume = 50f;

	[Header("VFX")]
	[SerializeField] float vibrateRadius = 0.1f;
	[SerializeField] int numberOfVibrations = 10;
	[SerializeField] float timeBetweenVibrations = .5f;

	[SerializeField] GameObject explosionVFX;
	[SerializeField] GameObject takeHitVFX;
	SpriteRenderer sr;
	[SerializeField] public bool moveMe = true;



	Coroutine firingCoroutine;
	
	float xMin, xMax, yMin, yMax;

	void Start()
	{
		sr = healthIndicator.GetComponent<SpriteRenderer>();
		originalHealth = health;
		SetUpMoveBoundaries();
	}

	public void dl(float powerupDuration)
	{
		StartCoroutine(setDoubleLasers(powerupDuration));
	}

	public IEnumerator setDoubleLasers(float powerupDuration)
	{
		numLasers += 1;
		yield return new WaitForSeconds(powerupDuration);
		int newNum = (int)numLasers - 1;
		if (newNum < 1)
		{
			newNum = 1;
		}

		numLasers = newNum;
	}
	
	void Update()
	{
		Move();	
		Fire();

		float floatHealth = health;
		float floatOriginalHealth = originalHealth;
		float newY = (floatHealth / floatOriginalHealth) * -4f;

		healthIndicator.transform.localScale = new Vector3(healthIndicator.transform.localScale.x, newY, healthIndicator.transform.localScale.z);
		float newValue = Mathf.Clamp(((float)health/(float)originalHealth), 0, 1);
		sr.color = new Color(1, newValue, newValue, 1);
		healthText.GetComponent<Text>().text = "%" + Mathf.Clamp(Mathf.RoundToInt(((float)health/(float)originalHealth)*100), 0, 100);
	}

	private void OnTriggerEnter2D(Collider2D otherThing)
	{
		if (otherThing.tag != "powerup")
		{
			health -= otherThing.GetComponent<DamageDealer>().GetDamageToDeal();
			otherThing.GetComponent<DamageDealer>().Hit();
			takeHit();

			if (health <= 0)
			{
				health = 0;
				Update();
				AudioSource.PlayClipAtPoint(explosionSound, transform.position, explodeVolume);
				Instantiate(explosionVFX, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}

	public void addToHealth(int amount)
	{
		health += amount;

		if (health > originalHealth)
		{
			health = originalHealth;
		}
	}

	private void takeHit()
	{
		AudioSource.PlayClipAtPoint(takeDamageSound, transform.position, takeDamageVolume);
		StartCoroutine(vibrate());
	}

	IEnumerator vibrate()
	{
		GameObject smallExplode = Instantiate(takeHitVFX, transform.position, Quaternion.identity);
		for (int x = 0; x < numberOfVibrations; x++)
		{
			Vector3 originalPosition = transform.position;
			float xJump = Random.Range(-vibrateRadius, vibrateRadius);
			float yJump = Random.Range(-vibrateRadius, vibrateRadius);
			transform.position = new Vector3(transform.position.x + xJump, transform.position.y + yJump, transform.position.z);
			yield return new WaitForSeconds(timeBetweenVibrations);
			transform.position = originalPosition;
			Move();
			Move();
		}
		Destroy(smallExplode);
	}

	private void Move()
	{
		var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

		var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
		var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

		if (moveMe)
		{
			gameObject.transform.position = new Vector2(newXPos, newYPos);
		}
	}

	private void SetUpMoveBoundaries()
	{
		Camera gameCamera = Camera.main;
		
		xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
		xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;	
		yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
		yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
	}

	private void Fire()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			firingCoroutine = StartCoroutine(FireContinuously());
		}
		if (Input.GetButtonUp("Fire1"))
		{
			StopCoroutine(firingCoroutine);
		}
	}

	IEnumerator FireContinuously()
	{
		while (true)
		{
			if (numLasers > 7)
			{
				numLasers = 7;
			}
			int numIntervals = (int)numLasers + 1;
			float intervalDist = 1.2f / (numLasers + 1f);
			Debug.Log("numIntervals: " + numIntervals + "\nintervalDist: " + intervalDist + "\nnumLasers: " + numLasers);

			for (int x = 1; x <= numIntervals; x++)
			{		
				if (x != numIntervals)
				{
					GameObject laser = Instantiate(laserPrefab, new Vector3((transform.position.x - 0.6f) + (intervalDist*x), transform.position.y, transform.position.z + .5f), Quaternion.identity) as GameObject;
					laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed + Random.Range(0,5f));
				}
			}

			AudioSource.PlayClipAtPoint(laserSound, transform.position, laserVolume);
			transform.position = new Vector3(transform.position.x, transform.position.y - numLasers/8f, transform.position.z);

			yield return new WaitForSeconds(timeBetweenLasers);
		}
	}

	public void dls(float powerupDuration)
	{
		StartCoroutine(doubleLaserSpeed(powerupDuration));
	}

	public IEnumerator doubleLaserSpeed(float powerupDuration)
	{
		float newTime = timeBetweenLasers /= 2;

		if (newTime < 0.075f)
		{
			newTime = 0.075f;
		}
		timeBetweenLasers = newTime;
		yield return new WaitForSeconds(powerupDuration);
		newTime *= 2;

		if (newTime > .2f)
		{
			newTime = .2f;
		}
		timeBetweenLasers = newTime;
	}
}

