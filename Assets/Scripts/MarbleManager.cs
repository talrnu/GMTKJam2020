using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;

	private void Update()
	{
		// Check to see if any marbles were destroyed
		CleanUpList();

		// If the marble count has hit one and the timer hasn't been set yet: Start the timer.
		if (CountActiveMarbles() <= 1 && !_gameManager.IsGameOver())
		{
			Debug.Log("Game over! Starting game over timer...");
			_gameManager.GameOver();
		}
	}
	
	private int CountActiveMarbles()
	{
		int count = 0;
		foreach (var marble in _gameManager.Marbles)
		{
			if (marble.activeSelf)
			{
				count++;
			}
		}
		
		return count;
	}

	private void CleanUpList()
	{
		for (var i = _gameManager.Marbles.Count - 1; i > -1; i--)
		{
			if (_gameManager.Marbles[i]?.GetComponent<Ball>()?.DestroyThisBall ?? false)
				_gameManager.Marbles[i].SetActive(false);
		}
	}
}
