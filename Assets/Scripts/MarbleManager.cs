using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	[SerializeField] public List<GameObject> ActiveMarbles; 

	private void Update()
	{
		// Check to see if any marbles were destroyed
		CleanUpList();

		// If the marble count has hit zero and the timer hasn't been set yet: Start the timer.
		if (ActiveMarbles.Count == 0 && !_gameManager.IsGameOver())
		{
			Debug.Log("No more marbles remain! Starting game over timer...");
			_gameManager.GameOver();
		}
	}

	private void CleanUpList()
	{
		for (var i = ActiveMarbles.Count - 1; i > -1; i--)
		{
			if (ActiveMarbles[i] == null)
				ActiveMarbles.RemoveAt(i);
		}
	}
}
