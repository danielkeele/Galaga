using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
	WaveConfig waveConfig;
	List<Transform> waypoints;
	int waypointIndex = 0;

	void Start()
	{
		waypoints = waveConfig.GetWaypoints();
		gameObject.transform.position = waypoints[waypointIndex].position;
	}

	void Update()
	{
		Move();
	}

	public void SetWaveConfig(WaveConfig waveConfig)
	{
		this.waveConfig = waveConfig;
	}

	private void Move()
	{
		if (waypointIndex < waypoints.Count)
		{
			var target = waypoints[waypointIndex].position;
			transform.position = Vector2.MoveTowards(transform.position, target, waveConfig.GetMoveSpeed() * Time.deltaTime);

			if (transform.position.x == target.x && transform.position.y == target.y)
			{
				waypointIndex++;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
