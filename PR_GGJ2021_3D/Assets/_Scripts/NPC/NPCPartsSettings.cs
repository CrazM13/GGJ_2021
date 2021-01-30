using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Settings", menuName = "NPC/Settings", order = 0)]
public class NPCPartsSettings : ScriptableObject {

	public Color[] clothesPallet;
	public Color[] extraPallet;

	public Material[] torsoStyles;
	public Material[] legStyles;

}
