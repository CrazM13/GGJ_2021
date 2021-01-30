using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>Contains ability to navigate map</summary>
*/
public abstract class NavigationMapActor : MonoBehaviour {



	[Tooltip("The amount of time it takes to cover 1 meter")]
	[SerializeField] private float speed = 1.4f;
	private AnimationCurve progressCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);



	private NavigationMapActorState actorState = NavigationMapActorState.STOPPED;

	private float pathTimeExpected = 0;
	private float pathTimeElapsed = 0;

	private NavigationMapNode targetNode;
	private NavigationMapNode previousNode;
	private Vector3 targetPos;
	private Vector3 prevPos = Vector3.zero;

	private Quaternion targetRotation = Quaternion.identity;

	public bool IsOnPath { get; private set; } = false;

	public bool IsNavigating => actorState == NavigationMapActorState.WALKING;

	public void SetTargetNode(NavigationMapNode newTargetNode) {
		this.targetNode = newTargetNode;

		actorState = NavigationMapActorState.WALKING;

		targetPos = this.targetNode.GetRandomPositionWithin();
		prevPos = transform.position;

		pathTimeElapsed = 0;
		pathTimeExpected = Vector3.Distance(prevPos, targetPos) * speed;

		IsOnPath = true;
	}

	public void TeleportToTargetNode(NavigationMapNode newTargetNode) {
		this.targetNode = newTargetNode;

		actorState = NavigationMapActorState.WALKING;

		targetPos = this.targetNode.GetRandomPositionWithin();
		prevPos = transform.position;

		pathTimeExpected = Vector3.Distance(prevPos, targetPos) * speed;
		pathTimeElapsed = pathTimeExpected;

		IsOnPath = true;
	}

	public void SetTargetPositionOffPath(Vector3 newTargetpos) {
		actorState = NavigationMapActorState.WALKING;

		targetPos = newTargetpos;
		prevPos = transform.position;

		pathTimeElapsed = 0;
		pathTimeExpected = Vector3.Distance(prevPos, targetPos) * speed;

		IsOnPath = false;
	}

	public void SetTargetRest(float restTime) {
		actorState = NavigationMapActorState.STOPPED;

		targetPos = transform.position;
		prevPos = transform.position;

		pathTimeElapsed = 0;
		pathTimeExpected = restTime;

		IsOnPath = true;
	}

	public Vector3 GetPathAtPercent(float percent) {
		return Vector3.Lerp(prevPos, targetPos, progressCurve.Evaluate(percent));
	}

	public Vector3 GetPathAtTime(float time) {
		return Vector3.Lerp(prevPos, targetPos, progressCurve.Evaluate(time / pathTimeExpected));
	}

	/**
	 * <summary>Call this on update to follow path</summary>
	*/
	public void ContinueOnPath() {
		pathTimeElapsed += Time.deltaTime;

		Vector3 newPosition = GetPathAtTime(pathTimeElapsed);
		Vector3 direction = newPosition - transform.position;
		transform.position = newPosition;

		if (direction.magnitude > 0) {
			targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 10);
		}

		if (pathTimeElapsed >= pathTimeExpected) {
			if (IsOnPath) {
				if (targetNode.GetNodeType() == NavigationMapNodeTypes.REST) {
					SetTargetRest(Random.Range(1.0f, 5.0f));
				} else {
					NavigationMapNode newTarget = null;

					for (int i = 3; i > 0; i--) {
						newTarget = targetNode.GetRandomConnectedNode();

						if (newTarget != previousNode) break;
					}

					previousNode = targetNode;
					SetTargetNode(newTarget);
				}
			} else {
				SetTargetNode(targetNode);
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected() {

		if (targetNode) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(targetNode.transform.position, 0.15f);
		}

		if (previousNode) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(previousNode.transform.position, 0.15f);
		}

		Gizmos.color = Color.green;
		Gizmos.DrawSphere(targetPos, 0.075f);
	}
#endif

}

public enum NavigationMapActorState {
	WALKING,
	STOPPED
}
