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
	}

	// Update is called once per frame
	void Update() {

	}

	public void ModifyHat(bool hasHat) {
		NPCLoadoutHelper.ModifyHat(storedLoadout, hasHat);
	}

	public void ModifyHatStyle(uint hatStyleIndex) {
		NPCLoadoutHelper.ModifyHatStyle(storedLoadout, hatStyleIndex);
	}

	public void ModifyBeard(bool hasBeard) {
		NPCLoadoutHelper.ModifyBeard(storedLoadout, hasBeard);
	}

	public void ModifyBeardStyle(uint beardStyleIndex) {
		NPCLoadoutHelper.ModifyBeardStyle(storedLoadout, beardStyleIndex);
	}

	public void ModifyTorsoType(uint torsoTypeIndex) {
		NPCLoadoutHelper.ModifyTorsoType(storedLoadout, torsoTypeIndex);
	}

	public void ModifyTorsoStyle(uint torsoStyleIndex) {
		NPCLoadoutHelper.ModifyTorsoStyle(storedLoadout, torsoStyleIndex);
	}

	public void ModifyLegsType(uint legsTypeIndex) {
		NPCLoadoutHelper.ModifyLegsType(storedLoadout, legsTypeIndex);
	}

	public void ModifyLegsStyle(uint legsStyleIndex) {
		NPCLoadoutHelper.ModifyLegsStyle(storedLoadout, legsStyleIndex);
	}

	public void ModifyBoots(bool hasBoots) {
		NPCLoadoutHelper.ModifyBoots(storedLoadout, hasBoots);
	}

	public void ModifyBootsStyle(uint bootsStyleIndex) {
		NPCLoadoutHelper.ModifyBootsStyle(storedLoadout, bootsStyleIndex);
	}

	public void ModifyGloves(bool hasGloves) {
		NPCLoadoutHelper.ModifyGloves(storedLoadout, hasGloves);
	}

	public void ModifyGlovesStyle(uint glovesStyleIndex) {
		NPCLoadoutHelper.ModifyGlovesStyle(storedLoadout, glovesStyleIndex);
	}
}
