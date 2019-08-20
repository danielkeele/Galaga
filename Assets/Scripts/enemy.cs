using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	[SerializeField] float health = 100f;
	float originalHealth;
	SpriteRenderer sr;
	[SerializeField] int pointValue = 100;
	[SerializeField] GameObject laserPrefab;
	[SerializeField] GameObject explosion;
	[SerializeField] float laserSpeed = 10f;
	[SerializeField] float laserTimer;
	[SerializeField] float minTimeBetweenLasers = 1f;
	[SerializeField] float maxTimeBetweenLasers = 5f;
	[SerializeField] float explodeVolume = 1f;
	[SerializeField] AudioClip explodeSound;
	[SerializeField] AudioClip laserSound;
	[SerializeField] float laserVolume = 1f;
	[Header("Powerups")]
	[SerializeField] List<GameObject> powerups;
	[SerializeField] int oneOutOfXChanceOfDroppingPowerup = 10;
	

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		originalHealth = health;
		laserTimer = Random.Range(minTimeBetweenLasers, maxTimeBetweenLasers);
	}

	void Update()
	{
		CountDownAndShoot();
	}

	private void CountDownAndShoot()
	{
		laserTimer -= Time.deltaTime;
		if (laserTimer <= 0)
		{
			Fire();
			laserTimer = Random.Range(minTimeBetweenLasers, maxTimeBetweenLasers);
		}
	}

	private void Fire()
	{
		var laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f), Quaternion.identity);
		AudioSource.PlayClipAtPoint(laserSound, transform.position, laserVolume);
		laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-1 * laserSpeed * Random.Range(.6f, 1f)));
	}

	private void OnTriggerEnter2D(Collider2D otherThing)
	{
		if (otherThing.gameObject.tag != "Player")
		{
			DamageDealer damageDealer = otherThing.gameObject.GetComponent<DamageDealer>();
			TakeHit(damageDealer);
		}
	}

	private void TakeHit(DamageDealer damageDealer)
	{
		health -= damageDealer.GetDamageToDeal();
		damageDealer.Hit();
		flashRed();
		transform.position = new Vector3(transform.position.x, transform.position.y + .25f, transform.position.z);

		if (health <= 0)
		{
			score score = FindObjectOfType<score>();
			score.addToScore(pointValue);
			var sparkles = Instantiate(explosion, transform.position,Quaternion.identity);
			AudioSource.PlayClipAtPoint(explodeSound, transform.position, explodeVolume);
			dropPowerup();
			Destroy(gameObject);
			Destroy(sparkles, 1f);
		}
	}

	private void dropPowerup()
	{
		int chance = Random.Range(0, oneOutOfXChanceOfDroppingPowerup);

		if (chance == 0)
		{
			chance = Random.Range(0, powerups.Count);
			Instantiate(powerups[chance], transform.position, Quaternion.identity);
		}
	}

	private void flashRed()
	{
		float newValue = (health/originalHealth);
		//Debug.Log("health: " + health + "\n" + "originalHealth: " + originalHealth + "\nnewValue: " + newValue);

		sr.color = new Color(1, newValue, newValue, 1);
	}
}
