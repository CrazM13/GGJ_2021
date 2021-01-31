using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

	private uint targetLoadout;

	[SerializeField] private NPCPartsSettings settings;

	[SerializeField] private NPCBase targetNPC;

	[SerializeField] private NPCBase[] otherNPCs;

	[SerializeField] private NavigationMapNode[] startingPositions;

	void Awake() {
		targetLoadout = NPCLoadoutHelper.CreateRandomLoadoutString();

		targetNPC.SetManager(this);
		targetNPC.SetLoadout(targetLoadout, settings);
		targetNPC.TeleportToTargetNode(startingPositions[Random.Range(0, startingPositions.Length)]);

		foreach (NPCBase npc in otherNPCs) {
			uint tmpLoadout;

			do {
				tmpLoadout = NPCLoadoutHelper.CreateRandomLoadoutString();
			} while (tmpLoadout == targetLoadout);

			npc.SetManager(this);
			npc.SetLoadout(tmpLoadout, settings);
			npc.TeleportToTargetNode(startingPositions[Random.Range(0, startingPositions.Length)]);
		}

	}

	void Update() {

	}

	public uint GetTargetLoadout() {
		return targetLoadout;
	}

}
