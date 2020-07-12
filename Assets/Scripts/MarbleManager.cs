using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleManager : MonoBehaviour
{
	[SerializeField] private GameManager _gameManager;
	private List<GameObject> _activeMarbles;

	private void Start()
	{
		_activeMarbles = new List<GameObject>();

		GameObject[] list = GameObject.FindGameObjectsWithTag("Ball");

		foreach (var marble in list)
		{
			_activeMarbles.Add(marble);
		}
	}

	private void Update()
	{
		// Check to see if any marbles were destroyed
		CleanUpList();

		// If the marble count has hit one and the timer hasn't been set yet: Start the timer.
		if (_activeMarbles.Count == 1 && !_gameManager.IsGameOver())
		{
			Debug.Log("No more marbles remain! Starting game over timer...");
			_gameManager.GameOver();
		}
	}

	private void CleanUpList()
	{
		for (var i = _activeMarbles.Count - 1; i > -1; i--)
		{
			if (_activeMarbles[i] == null)
				_activeMarbles.RemoveAt(i);
			else if (_activeMarbles[i]?.GetComponent<Ball>()?.DestroyThisBall ?? false)
				Object.Destroy(_activeMarbles[i]);
			
		}
	}
}
