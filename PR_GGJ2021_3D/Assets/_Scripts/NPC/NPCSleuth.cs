﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSleuth : NPCBase {

	[SerializeField] private TabletUI tablet;

	public override string OnInteract() {
		return "Talk to me when you know who it is";
	}

	public override void OnArrest() {
		Debug.Log("Target => " + NPCLoadoutHelper.ToString(manager.GetTargetLoadout()));
		Debug.Log("Tablet => " + NPCLoadoutHelper.ToString(tablet.GetStoredLoadout()));

		if (tablet.GetStoredLoadout() == manager.GetTargetLoadout()) {
			ScenesManager.instance.Win();
		} else {
			ScenesManager.instance.Lose();
		}
	}

}
