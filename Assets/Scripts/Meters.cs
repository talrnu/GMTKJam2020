using System;
using UnityEngine;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	private Text _meterPoints;
	private string _warningColor = "red";
	private Color _normalColor = Color.green;
	
	void Start()
	{
		_meterPoints = GameObject.Find("MeterPoints").GetComponent<Text>();
		_meterPoints.color = _normalColor;
		_meterPoints.text = "Roll out";
	}

	void OnGUI ()
	{
		var balls = GameObject.FindGameObjectsWithTag("Ball");
		
		_meterPoints.text = "";
		int i = 0;
		foreach (var ball in balls)
		{
			var ballComponent = ball.GetComponent<Ball>();
			var warning = ballComponent.OneSecondToSelfDestruct;
			_meterPoints.text += warning ? "<color=" + _warningColor + ">" : "";
			_meterPoints.text += "Points[" + i + "]:" + ballComponent.Points;
			_meterPoints.text += warning ? "</color>" : "";
			i++;
		}
	}
}
