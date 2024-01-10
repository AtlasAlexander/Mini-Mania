using System;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
	[SerializeField] private Vector3[] points;

	public int ControlPointCount
	{
		get
		{
			return points.Length;
		}
	}


	public Vector3 GetControlPoint(int index)
	{
		return points[index];
	}


	public void SetControlPoint(int index, Vector3 point)
	{
		points[index] = point;
		EnforceMode(index);
	}


	private void EnforceMode(int index)
	{
		int modeIndex = (index + 1) / 3;

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;

		if (index <= middleIndex)
		{
			fixedIndex = middleIndex - 1;
			enforcedIndex = middleIndex + 1;
		}
		else
		{
			fixedIndex = middleIndex + 1;
			enforcedIndex = middleIndex - 1;
		}

		Vector3 middle = points[middleIndex];
		Vector3 enforcedTangent = middle - points[fixedIndex];

		enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);

		points[enforcedIndex] = middle + enforcedTangent;
	}


	public Vector3 GetPoint(float t)
	{
		int i;
		if (t >= 1f)
		{
			t = 1f;
			i = points.Length - 4;
		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetPoint(
			points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}


	public Vector3 GetVelocity(float t)
	{
		int i;
		if (t >= 1f)
		{
			t = 1f;
			i = points.Length - 4;
		}
		else
		{
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.CalculateCubicBezier(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}


	public Vector3 GetDirection(float t)
	{
		return GetVelocity(t).normalized;
	}


	public void AddCurve()
	{
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1f;
		points[points.Length - 3] = point;
		point.x += 1f;
		points[points.Length - 2] = point;
		point.x += 1f;
		points[points.Length - 1] = point;

		EnforceMode(points.Length - 4);
	}


	public int CurveCount
	{
		get
		{
			return (points.Length - 1) / 3;
		}
	}


	public void Reset()
	{
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
	}
}