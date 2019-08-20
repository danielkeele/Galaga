using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] List<WaveConfig> waves;
	[SerializeField] bool loop = false;
	int startingWave = 0;

	IEnumerator Start()
	{
		do
		{
			yield return StartCoroutine(SpawnAllWaves());
		}
		while (loop);
	}

	private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
	{
		for (int x = 0; x < wave.GetNumberOfEnemies(); x++)
		{
			var newEnemy = Instantiate(wave.GetEnemeyPrefab(), wave.GetWaypoints()[0].position, Quaternion.identity);

			newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);

			yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
		}
	}

	private IEnumerator SpawnAllWaves()
	{
		for (int x = startingWave; x < waves.Count; x++)
		{
			yield return StartCoroutine(SpawnAllEnemiesInWave(waves[x]));
			if (waves[x].GetTimeUntilNextWave() != 0f)
			{
				yield return new WaitForSeconds(waves[x].GetTimeUntilNextWave());
			}
		}
	}
}
