using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : NavigationMapActor {

	[SerializeField] private float loyalty;
	[SerializeField] private NavigationMapNode forcedStartNode;

	[SerializeField] private GameObject hatModel;
	[SerializeField] private GameObject beardModel;
	[SerializeField] private SkinnedMeshRenderer baseModel;

	[SerializeField] protected NPCManager manager;

	private Animator animator;

	private string storedLine = null;

	void Start() {
		if (forcedStartNode) TeleportToTargetNode(forcedStartNode);

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

		//Debug.Log(NPCLoadoutHelper.ToString(loadoutString));

		if (NPCLoadoutHelper.HasHat(loadoutString)) {
			uint style = NPCLoadoutHelper.GetHatStyle(loadoutString);

			// Apply hat model
			hatModel.SetActive(true);

			hatModel.GetComponent<MeshRenderer>().material.color = settings.clothesPallet[style];
		} else {
			hatModel.SetActive(false);
		}

		if (NPCLoadoutHelper.HasBeard(loadoutString)) {
			uint style = NPCLoadoutHelper.GetBeardStyle(loadoutString);

			// Apply beard model
			beardModel.SetActive(true);

			beardModel.GetComponent<MeshRenderer>().material.color = settings.clothesPallet[style];
		} else {
			beardModel.SetActive(false);
		}

		{
			uint type = NPCLoadoutHelper.GetTorsoStyle(loadoutString);
			uint style = NPCLoadoutHelper.GetTorsoStyle(loadoutString);

			// Apply torso model
			baseModel.materials[0].color = settings.clothesPallet[style];
		}

		{
			uint type = NPCLoadoutHelper.GetLegsStyle(loadoutString);
			uint style = NPCLoadoutHelper.GetLegsStyle(loadoutString);

			// Apply legs model
			baseModel.materials[3].color = settings.clothesPallet[style];
		}

		if (NPCLoadoutHelper.HasBoots(loadoutString)) {
			uint style = NPCLoadoutHelper.GetBootsStyle(loadoutString);

			// Apply boots model
			baseModel.materials[1].color = settings.extraPallet[style];
		}

		if (NPCLoadoutHelper.HasGloves(loadoutString)) {
			uint style = NPCLoadoutHelper.GetGlovesStyle(loadoutString);

			// Apply gloves model
			baseModel.materials[4].color = settings.extraPallet[style];
		}
	}

	public virtual string OnInteract() {
		return storedLine;
	}

	public virtual void OnArrest() {
		ScenesManager.instance.Lose();
	}

	public void StoreDialogue(string lines) {
		storedLine = lines;
	}

}
