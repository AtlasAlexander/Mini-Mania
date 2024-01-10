using UnityEngine;

public class SplineCamera : MonoBehaviour
{
	[HideInInspector] public Camera splineCamera;
	[HideInInspector] public Camera playerCamera;

	public GameObject playerTarget;
	public BezierSpline spline;
	public float duration = 10;

	[HideInInspector] public float progress;
	private bool goingForward = true;

	private void Awake()
	{
		splineCamera = gameObject.GetComponent<Camera>();
		playerCamera = GameObject.Find("IT_Player").GetComponentInChildren<Camera>();
	}

	private void LateUpdate()
	{
		if (goingForward)
		{
			progress += Time.deltaTime / duration;
			if (progress >= 1)
			{
				progress = 2 - progress;
				goingForward = false;
			}
		}
		else
		{
            progress -= Time.deltaTime / duration;
            if (progress <= 0)
            {
                progress = -progress;
                goingForward = true;
            }
        }

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		transform.LookAt(playerTarget.transform);
	}
}