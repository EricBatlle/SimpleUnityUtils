using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LerpAlongPath : MonoBehaviour
{
	public Transform projectileTransform = null;
	public float duration = 10.0f;//ten seconds.
	public List<Vector3> pathPoints = new List<Vector3>();

	[SerializeField] private bool runTimer = false;
	[SerializeField] private float currentTime = 0.0f;
	[SerializeField] private float stepSize = 0;
	[SerializeField] private int startPoint = 0;
	[SerializeField] private float lerpFactor = 0;

	[ContextMenu("StartMove")]
	private void StartMoving()
	{
		StartCoroutine(MoveProjectileAlongPath());
	}

	private void StartMoving(Transform transformToMove, float duration)
	{
		this.projectileTransform = transformToMove;
		this.duration = duration;
		StartCoroutine(MoveProjectileAlongPath());
	}

	private void Update()
	{
		if (runTimer)
		{
			currentTime += Time.deltaTime;
		}
	}

	public IEnumerator MoveProjectileAlongPath(Transform transformToMove, float duration, float initialPercent = 0, float finalPercent = 100, Action OnBeforeFirstMove = null, Action OnFirstMove = null, Action OnReachEnd = null)
	{
		bool isFirstMove = true;
		this.projectileTransform = transformToMove;
		this.duration = duration;
		int initialIndex = (int)(pathPoints.Count * (initialPercent / 100));
		pathPoints = pathPoints.GetRange(initialIndex, (int)(pathPoints.Count * (finalPercent / 100)) - initialIndex).ToList();
		currentTime = 0;
		runTimer = true;
		OnBeforeFirstMove?.Invoke();
		while (!((currentTime >= duration) || (duration <= 0.0f) || (pathPoints.Count == 0)))
		{
			stepSize = duration / (pathPoints.Count - 1);
			startPoint = Mathf.FloorToInt(currentTime / stepSize);
			lerpFactor = Mathf.Repeat(currentTime, stepSize) / stepSize;
			Vector3 position = Vector3.Lerp(
				pathPoints[startPoint],
				pathPoints[startPoint + 1],
				lerpFactor
			);
			projectileTransform.position = position;
			if(isFirstMove)
			{
				OnFirstMove?.Invoke();
				isFirstMove = false;
			}
			yield return null;
		}
		runTimer = false;
		projectileTransform.position = pathPoints[pathPoints.Count - 1];

		OnReachEnd?.Invoke();
	}

	public IEnumerator MoveProjectileAlongPath(Transform transformToMove, float duration, float initialPercent = 0, float finalPercent = 100, Action OnReachEnd = null)
	{
		this.projectileTransform = transformToMove;
		this.duration = duration;
		int initialIndex = (int)(pathPoints.Count * (initialPercent / 100));
		pathPoints = pathPoints.GetRange(initialIndex, (int)(pathPoints.Count * (finalPercent / 100)) - initialIndex).ToList();
		currentTime = 0;
		runTimer = true;
		while (!((currentTime >= duration) || (duration <= 0.0f) || (pathPoints.Count == 0)))
		{
			stepSize = duration / (pathPoints.Count - 1);
			startPoint = Mathf.FloorToInt(currentTime / stepSize);
			lerpFactor = Mathf.Repeat(currentTime, stepSize) / stepSize;
			Vector3 position = Vector3.Lerp(
				pathPoints[startPoint],
				pathPoints[startPoint + 1],
				lerpFactor
			);
			projectileTransform.position = position;
			
			yield return null;
		}
		runTimer = false;
		projectileTransform.position = pathPoints[pathPoints.Count - 1];

		OnReachEnd?.Invoke();
	}

	public IEnumerator MoveProjectileAlongPath()
	{
		currentTime = 0;
		runTimer = true;
		while (!((currentTime >= duration) || (duration <= 0.0f) || (pathPoints.Count == 0)))
		{
			stepSize = duration / (pathPoints.Count - 1);
			startPoint = Mathf.FloorToInt(currentTime / stepSize);
			lerpFactor = Mathf.Repeat(currentTime, stepSize) / stepSize;
			Vector3 position = Vector3.Lerp(
				pathPoints[startPoint],
				pathPoints[startPoint + 1],
				lerpFactor
			);
			projectileTransform.position = position;
			yield return null;
		}
		projectileTransform.position = pathPoints[pathPoints.Count - 1];
		runTimer = false;
	}
}
