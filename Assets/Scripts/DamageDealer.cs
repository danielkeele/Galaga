﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
	[SerializeField] int damageToDeal = 100;

	public int GetDamageToDeal()
	{
		return damageToDeal;
	}

	public void Hit()
	{
		if (gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
