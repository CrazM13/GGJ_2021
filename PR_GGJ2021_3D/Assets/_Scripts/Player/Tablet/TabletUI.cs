using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TabletUI : MonoBehaviour, IPointerClickHandler {

	private RectTransform rectTransform;
	[SerializeField] private Camera renderingCamera;

	private CanvasGroup openGroup;

	[SerializeField] private NPCBase npc;
	[SerializeField] private NPCPartsSettings settings;

	private uint storedLoadout = 0;

	public void OnPointerClick(PointerEventData eventData) {
		if (openGroup) {
			openGroup.alpha = 0;
			openGroup.interactable = false;
			openGroup.blocksRaycasts = false;

			openGroup = null;
			return;
		}

		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out Vector2 localClick);

		Vector2 viewportClick = new Vector2(localClick.x / rectTransform.rect.width, localClick.y / (rectTransform.rect.height));

		Ray worldRay = renderingCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

		if (Physics.Raycast(worldRay, out RaycastHit hit, 100)) {
			TabletClickDetection detection = hit.collider.GetComponent<TabletClickDetection>();

			

			if (detection) {
				openGroup = detection.popup;

				openGroup.alpha = 1;
				openGroup.interactable = true;
				openGroup.blocksRaycasts = true;
			}
		}
	}

	// Start is called before the first frame update
	void Start() {
		rectTransform = GetComponent<RectTransform>();

		storedLoadout = NPCLoadoutHelper.CreateLoadoutString(false, 0, false, 0, 3, 0, 3, 0, true, 0, true, 0);
		npc.SetLoadout(storedLoadout, settings);
	}

	// Update is called once per frame
	void Update() {

	}

	public void ModifyHat(bool hasHat) {
		storedLoadout = NPCLoadoutHelper.ModifyHat(storedLoadout, hasHat);
		if (!hasHat) storedLoadout = NPCLoadoutHelper.ModifyHatStyle(storedLoadout, 0);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyHatStyle(uint hatStyleIndex) {
		if (!NPCLoadoutHelper.HasHat(storedLoadout)) return;

		storedLoadout = NPCLoadoutHelper.ModifyHatStyle(storedLoadout, hatStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyBeard(bool hasBeard) {
		storedLoadout = NPCLoadoutHelper.ModifyBeard(storedLoadout, hasBeard);
		if (!hasBeard) storedLoadout = NPCLoadoutHelper.ModifyBeardStyle(storedLoadout, 0);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyBeardStyle(uint beardStyleIndex) {
		if (!NPCLoadoutHelper.HasBeard(storedLoadout)) return;

		storedLoadout = NPCLoadoutHelper.ModifyBeardStyle(storedLoadout, beardStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyTorsoType(uint torsoTypeIndex) {
		storedLoadout = NPCLoadoutHelper.ModifyTorsoType(storedLoadout, torsoTypeIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyTorsoStyle(uint torsoStyleIndex) {
		//if (NPCLoadoutHelper.GetTorsoType(storedLoadout) == 0) return;

		storedLoadout = NPCLoadoutHelper.ModifyTorsoStyle(storedLoadout, torsoStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyLegsType(uint legsTypeIndex) {
		storedLoadout = NPCLoadoutHelper.ModifyLegsType(storedLoadout, legsTypeIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyLegsStyle(uint legsStyleIndex) {
		//if (NPCLoadoutHelper.GetLegsType(storedLoadout) == 0) return;

		storedLoadout = NPCLoadoutHelper.ModifyLegsStyle(storedLoadout, legsStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyBoots(bool hasBoots) {
		//storedLoadout = NPCLoadoutHelper.ModifyBoots(storedLoadout, hasBoots);
		//if (!hasBoots) storedLoadout = NPCLoadoutHelper.ModifyBootsStyle(storedLoadout, 0);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyBootsStyle(uint bootsStyleIndex) {
		//if (!NPCLoadoutHelper.HasBoots(storedLoadout)) return;

		storedLoadout = NPCLoadoutHelper.ModifyBootsStyle(storedLoadout, bootsStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyGloves(bool hasGloves) {
		//storedLoadout = NPCLoadoutHelper.ModifyGloves(storedLoadout, hasGloves);
		//if (!hasGloves) storedLoadout = NPCLoadoutHelper.ModifyGlovesStyle(storedLoadout, 0);
		npc.SetLoadout(storedLoadout, settings);
	}

	public void ModifyGlovesStyle(uint glovesStyleIndex) {
		//if (!NPCLoadoutHelper.HasGloves(storedLoadout)) return;

		storedLoadout = NPCLoadoutHelper.ModifyGlovesStyle(storedLoadout, glovesStyleIndex);
		npc.SetLoadout(storedLoadout, settings);
	}

	public uint GetStoredLoadout() {
		return storedLoadout;
	}
}
