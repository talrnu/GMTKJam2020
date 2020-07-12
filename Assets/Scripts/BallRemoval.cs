using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRemoval : MonoBehaviour
{
	[SerializeField] private int _pointsToAdd = 0;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			// TODO for Lisa (FX): Before the destroy line we can probably exectue effects here.
			other.GetComponent<Ball>().Points += _pointsToAdd;

			other.GetComponent<Ball>().DestroyThisBall = true;
		}
	}
}
