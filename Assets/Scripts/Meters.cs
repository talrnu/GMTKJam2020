using System;
using UnityEngine;
using UnityEngine.UI;

public class Meters : MonoBehaviour
{
	private Text _meterPoints;
	private string _statonaryColor = "orange";
	private string _warningColor = "red";
	private Color _normalColor = Color.blue;
	
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
			var statonary = ballComponent.TimeToSelfDestruct <= ballComponent.SelfDestructSeconds - 1;
			var warning = ballComponent.TimeToSelfDestruct <= 1;
			_meterPoints.text += warning ? "<color=" + _warningColor + ">" : statonary ? "<color=" + _statonaryColor + ">" : "";
			_meterPoints.text += "Points[" + i + "]:" + ballComponent.Points;
		_meterPoints.text += (warning || statonary) ? "</color>" : "";
			i++;
		}
	}
}
