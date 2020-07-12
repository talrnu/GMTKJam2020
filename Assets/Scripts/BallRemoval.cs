﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRemoval : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			// TODO for Lisa (FX): Before the destroy line we can probably exectue effects here.

			Object.Destroy(other.gameObject);
			Debug.Log("A marble has been destroyed!");
		}
	}
}