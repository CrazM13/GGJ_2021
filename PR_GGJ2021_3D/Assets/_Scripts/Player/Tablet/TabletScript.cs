using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletScript : MonoBehaviour {

	private float animTimeElapsed = 0;
	private bool isAnimating = false;

	[SerializeField] private AnimationCurve animationCurve;

	[SerializeField] private Vector3 openModelPosition;
	[SerializeField] private Vector3 closedModelPosition;

	[SerializeField] private Vector3 openModelScale;
	[SerializeField] private Vector3 closedModelScale;

	[SerializeField] private Transform modelTransform;
	[SerializeField] private CanvasGroup tabletUI;

	public bool IsOpen { get; private set; } = false;

	void Start() {

	}

	void Update() {
		if (isAnimating) {
			animTimeElapsed += Time.deltaTime * (IsOpen ? 1 : -1);

			modelTransform.localPosition = GetPosition(animTimeElapsed);
			modelTransform.localScale = GetScale(animTimeElapsed);

			if (animTimeElapsed < 0 || animTimeElapsed > 1) {
				isAnimating = false;
				if (IsOpen) {
					tabletUI.alpha = 1;
					tabletUI.interactable = true;
					tabletUI.blocksRaycasts = true;
				}
			}

			animTimeElapsed = Mathf.Clamp01(animTimeElapsed);
		}
	}

	private Vector3 GetPosition(float percent) {
		return Vector3.Lerp(closedModelPosition, openModelPosition, animationCurve.Evaluate(percent));
	}

	private Vector3 GetScale(float percent) {
		return Vector3.Lerp(closedModelScale, openModelScale, animationCurve.Evaluate(percent));
	}

	public void Open() {
		if (isAnimating) return;

		isAnimating = true;
		IsOpen = true;

		animTimeElapsed = 0;
	}

	public void Close() {
		if (isAnimating) return;

		isAnimating = true;
		IsOpen = false;

		animTimeElapsed = 1;
	}

}
