using UnityEngine;
using System.Collections;

public class HardPoint
{
	// Clockwise positive, 0-359, 0 = straight up
	private float angle;
	private Vector2 location;

	public Vector2 Location
	{
		get
		{
			return location;
		}
	}

	public float Angle
	{
		get
		{
			return angle;
		}
	}

	public HardPoint(Vector2 inputLocation, float inputAngle)
	{
		location = inputLocation;
		angle = inputAngle % 360f;
	}

}
