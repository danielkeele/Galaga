using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
	[SerializeField] float rightSpinSpeed = 1f;
	[SerializeField] float flipSpeed = 1f;
	
	void Update()
	{
		gameObject.transform.Rotate(Vector3.right * flipSpeed);
		gameObject.transform.Rotate(Vector3.back * rightSpinSpeed);
	}
}
