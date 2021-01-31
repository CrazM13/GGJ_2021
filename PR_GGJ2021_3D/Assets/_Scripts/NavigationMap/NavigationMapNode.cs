using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NavigationMapNode : MonoBehaviour {

	[SerializeField] private NavigationMapAreas area;
	[SerializeField] private NavigationMapNodeTypes nodeType;

	[SerializeField] private float nodeRadius = 0;

	[SerializeField] private NavigationMapNode[] connections = new NavigationMapNode[0];

	[SerializeField] private bool canStop = false;
	[SerializeField, Range(1, 100)] private int chanceToStop = 1;
	[SerializeField] private NavigationMapNode[] stopNodes = new NavigationMapNode[0];

	public NavigationMapNode GetRandomConnectedNode(bool allowStoppingNodes = false) {
		bool shouldStop = canStop && allowStoppingNodes && Random.Range(1, 101) <= chanceToStop;

		if (shouldStop) {
			return stopNodes[Random.Range(0, stopNodes.Length)];
		}

		return connections[Random.Range(0, connections.Length)];
	}

	public NavigationMapNodeTypes GetNodeType() {
		return nodeType;
	}

	public Vector3 GetRandomPositionWithin() {
		Vector2 offset2D = Random.insideUnitCircle * nodeRadius;
		Vector3 offset3D = new Vector3(offset2D.x, 0, offset2D.y);
		return transform.position + offset3D;
	}

	public int GetNumberOfConnectedNodes() {
		return connections.Length + (canStop ? stopNodes.Length : 0);
	}

#if UNITY_EDITOR

	private void OnDrawGizmos() {
		Color nodeColour = nodeType == NavigationMapNodeTypes.REST ? Color.red : Color.white;

		Gizmos.color = nodeColour;
		Gizmos.DrawSphere(transform.position, 0.1f);

		Handles.color = nodeColour;
		Handles.DrawWireDisc(transform.position, Vector3.up, nodeRadius);

		foreach (NavigationMapNode connection in connections) {
			if (connection == null) continue;

			Gizmos.color = connection.GetNodeType() == NavigationMapNodeTypes.REST ? Color.red : nodeColour;
			Gizmos.DrawLine(transform.position, connection.transform.position);
			DrawRay(transform.position, (connection.transform.position - transform.position).normalized, Color.white);
		}

		Gizmos.color = nodeColour;
		foreach (NavigationMapNode stop in stopNodes) {
			if (stop == null) continue;

			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, stop.transform.position);
			DrawRay(transform.position, (stop.transform.position - transform.position).normalized, Color.red);
		}
	}

	private static void DrawRay(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f) {
		if (direction.magnitude <= 0) return;

		Color storedColour = Gizmos.color;
		Gizmos.color = color;
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
		Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, left * arrowHeadLength);

		Gizmos.color = storedColour;
	}

#endif

}

[System.Serializable]
public class NavigationMapNodeConnection {
	[SerializeField] public NavigationMapNode connection;
	[SerializeField,Min(1)] public int weight = 1;

	public NavigationMapNodeConnection() {
		connection = null;
		weight = 1;
	}
}

public enum NavigationMapNodeTypes {
	WALK, REST
}
