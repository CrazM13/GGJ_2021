using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTarget : NPCBase {

	public override void OnArrest() {
		ScenesManager.instance.Win();
	}

}
