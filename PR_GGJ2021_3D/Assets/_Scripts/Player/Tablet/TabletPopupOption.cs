using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabletPopupOption : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private TabletUI tabletUI;

	[SerializeField] private NPCLoadoutElements element;
	[SerializeField] private uint newValue;

	public void OnPointerClick(PointerEventData eventData) {

		switch (element) {
			case NPCLoadoutElements.HAT:
				tabletUI.ModifyHat(newValue == 1);
				break;
			case NPCLoadoutElements.HAT_STYLE:
				tabletUI.ModifyHatStyle(newValue);
				break;
			case NPCLoadoutElements.BEARD:
				tabletUI.ModifyBeard(newValue == 1);
				break;
			case NPCLoadoutElements.BEARD_STYLE:
				tabletUI.ModifyBeardStyle(newValue);
				break;
			case NPCLoadoutElements.TORSO:
				tabletUI.ModifyTorsoType(newValue);
				break;
			case NPCLoadoutElements.TORSO_STYLE:
				tabletUI.ModifyTorsoStyle(newValue);
				break;
			case NPCLoadoutElements.LEGS:
				tabletUI.ModifyLegsType(newValue);
				break;
			case NPCLoadoutElements.LEGS_STYLE:
				tabletUI.ModifyLegsStyle(newValue);
				break;
			case NPCLoadoutElements.BOOTS:
				tabletUI.ModifyBoots(newValue == 1);
				break;
			case NPCLoadoutElements.BOOTS_STYLE:
				tabletUI.ModifyBootsStyle(newValue);
				break;
			case NPCLoadoutElements.GLOVES:
				tabletUI.ModifyGloves(newValue == 1);
				break;
			case NPCLoadoutElements.GLOVES_STYLE:
				tabletUI.ModifyGlovesStyle(newValue);
				break;
		}
	}
}
