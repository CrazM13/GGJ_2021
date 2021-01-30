using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : NavigationMapActor {

	[SerializeField] private float loyalty;

	private NPCManager manager;

	private Animator animator;

	void Start() {
		loyalty = Random.value;
		animator = GetComponent<Animator>();
	}

	void Update() {
		animator.SetBool("walking", IsNavigating);

		if (IsNavigating) ContinueOnPath();
	}

	public float GetLoyalty() {
		return loyalty;
	}

	public void SetManager(NPCManager manager) {
		this.manager = manager;
	}

	public void SetLoadout(uint loadoutString, NPCPartsSettings settings) {
		if (NPCLoadoutHelper.HasHat(loadoutString)) {
			uint style = NPCLoadoutHelper.GetHatStyle(loadoutString);

			// Apply hat model
		}

		if (NPCLoadoutHelper.HasBeard(loadoutString)) {
			uint style = NPCLoadoutHelper.GetBeardStyle(loadoutString);

			// Apply beard model
		}

		{
			uint type = NPCLoadoutHelper.GetTorsoStyle(loadoutString);
			uint style = NPCLoadoutHelper.GetTorsoStyle(loadoutString);

			// Apply torso model
		}

		{
			uint type = NPCLoadoutHelper.GetLegsStyle(loadoutString);
			uint style = NPCLoadoutHelper.GetLegsStyle(loadoutString);

			// Apply legs model
		}

		if (NPCLoadoutHelper.HasBoots(loadoutString)) {
			uint style = NPCLoadoutHelper.GetBootsStyle(loadoutString);

			// Apply boots model
		}

		if (NPCLoadoutHelper.HasGloves(loadoutString)) {
			uint style = NPCLoadoutHelper.GetGlovesStyle(loadoutString);

			// Apply gloves model
		}
	}

}
