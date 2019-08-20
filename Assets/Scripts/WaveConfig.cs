using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject pathPrefab;
	[SerializeField] float timeBetweenSpawns = .5f;
	[SerializeField] float spawnRandomFactor = .3f;
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] int numberOfEnemies = 5;
	[SerializeField] float timeUntilNextWave = 5f;

	public GameObject GetEnemeyPrefab()
	{
		return enemyPrefab;
	}

	public GameObject GetPathPrefab()
	{
		return pathPrefab;
	}

	public List<Transform> GetWaypoints()
	{
		var waveWaypoints = new List<Transform>();

		foreach (Transform item in pathPrefab.transform)
		{
			waveWaypoints.Add(item);
		}
		
		return waveWaypoints;
	}
	public float GetTimeBetweenSpawns()
	{
		return timeBetweenSpawns;
	}

	public float GetSpawnRandomFactor()
	{
		return spawnRandomFactor;
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}

	public float GetTimeUntilNextWave()
	{
		return timeUntilNextWave;
	}

	public int GetNumberOfEnemies()
	{
		return numberOfEnemies;
	}

}
