using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabletClose : MonoBehaviour, IPointerClickHandler {

	[SerializeField] private TabletScript tabletModel;
	[SerializeField] private CanvasGroup tabletUI;

	public void OnPointerClick(PointerEventData eventData) {
		tabletModel.Close();
		tabletUI.alpha = 0;
		tabletUI.interactable = false;
		tabletUI.blocksRaycasts = false;
	}
}
