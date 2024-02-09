using UnityEngine;

public class SplineWalker : MonoBehaviour
{
	public GameObject splineWalker;

	public GameObject playerTarget;
	public BezierSpline spline;
	public float duration = 10;

	[HideInInspector] public float progress;
	private bool goingForward = true;
	public bool active = false;

    private void LateUpdate()
	{
		if (active)
        {
			if (goingForward)
			{
				progress += Time.deltaTime / duration;
				if (progress >= 1)
				{
					active = false;

					//ping pong
					//progress = 2 - progress;
					//goingForward = false;
				}
			}

			Vector3 position = spline.GetPoint(progress);
			transform.localPosition = position;
			transform.LookAt(playerTarget.transform);
		}
		else
        {
			progress = 0;
        }
	}
}