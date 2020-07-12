using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public float MovementEpsilon = 0.05f;
	public int SelfDestructSeconds = 5;
	public string Name = "Default";
	
	[HideInInspector]
	public bool DestroyThisBall = false;
	[HideInInspector]
	public int Points = 0;
	[HideInInspector]
	public int TimeToSelfDestruct;

	private Vector3 _currentPosition;
	private Vector3 _previousPosition;
	private bool _timerStarted = false;

	[SerializeField] private AudioSource _movingSound;

	void Start()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		_previousPosition = _currentPosition;
		TimeToSelfDestruct = SelfDestructSeconds;
		
		if (!_timerStarted)
		{
			_timerStarted = true;
			Invoke ( "_tick", 1f );
		}

		_movingSound.Play();
	}

	private void Update()
	{
		_currentPosition = this.GetComponent<Transform>().position;
		float dist = Vector3.Distance(_previousPosition, _currentPosition);
		if (Vector3.Distance(_previousPosition, _currentPosition) > MovementEpsilon)
		{
			if (!_movingSound.isPlaying)
				_movingSound.Play();

			TimeToSelfDestruct = SelfDestructSeconds;
		}

		_previousPosition = _currentPosition;
		
		if (TimeToSelfDestruct <= 0)
		{
			DestroyThisBall = true;
			_movingSound.Stop();
		}
	}
	
	private void _tick()
	{
		if (!DestroyThisBall)
		{
			Points++;
			TimeToSelfDestruct--;

			Invoke ( "_tick", 1f );
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Hole"))
		{
			int HoleSound = UnityEngine.Random.Range(1, 3);
			if (HoleSound == 1)
				SoundManager.Instance.Playsound("FX/Marble Falls In Glass Hole 1");
			if (HoleSound == 2)
				SoundManager.Instance.Playsound("FX/Marble Falls In Glass Hole 2");
			if (HoleSound == 3)
				SoundManager.Instance.Playsound("FX/Marble Falls In Glass Hole 3");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ball"))
		{
			SoundManager.Instance.Playsound("FX/Marble Hits Metal 1");
		}
		else if (collision.gameObject.CompareTag("Wall"))
		{
			int WallSound = UnityEngine.Random.Range(1, 4);
			if (WallSound == 1)
				SoundManager.Instance.Playsound("FX/Marble Hits Glass 1");
			if (WallSound == 2)
				SoundManager.Instance.Playsound("FX/Marble Hits Glass 2");
			if (WallSound == 3)
				SoundManager.Instance.Playsound("FX/Marble Hits Glass 3");
			if (WallSound == 4)
				SoundManager.Instance.Playsound("FX/Marble Hits Glass 4");
		}
	}
}
