using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class NPCLoadoutHelper {
	#region Decode
	public static bool HasHat(uint loadout) {
		return ((loadout & ((uint) NPCLoadoutElements.HAT)) >> 29) == 1;
	}

	public static uint GetHatStyle(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.HAT_STYLE)) >> 25;
	}

	public static bool HasBeard(uint loadout) {
		return ((loadout & ((uint) NPCLoadoutElements.BEARD)) >> 24) == 1;
	}

	public static uint GetBeardStyle(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.BEARD_STYLE)) >> 20;
	}

	public static uint GetTorsoType(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.TORSO)) >> 18;
	}

	public static uint GetTorsoStyle(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.TORSO_STYLE)) >> 14;
	}

	public static uint GetLegsType(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.LEGS)) >> 12;
	}

	public static uint GetLegsStyle(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.LEGS_STYLE)) >> 8;
	}

	public static bool HasBoots(uint loadout) {
		return ((loadout & ((uint) NPCLoadoutElements.BOOTS) >> 7)) == 1;
	}

	public static uint GetBootsStyle(uint loadout) {
		return (loadout & ((uint) NPCLoadoutElements.BOOTS_STYLE)) >> 4;
	}

	public static bool HasGloves(uint loadout) {
		return ((loadout & ((uint) NPCLoadoutElements.GLOVES) >> 3)) == 1;
	}

	public static uint GetGlovesStyle(uint loadout) {
		return loadout & ((uint) NPCLoadoutElements.GLOVES_STYLE);
	}


	public static string ToString(uint loadout) {
		StringBuilder stringBuilder = new StringBuilder();

		stringBuilder.AppendLine($"Has Hat: {HasHat(loadout)}");
		stringBuilder.AppendLine($"Hat Style: {GetHatStyle(loadout)}");

		stringBuilder.AppendLine($"Has Beard: {HasBeard(loadout)}");
		stringBuilder.AppendLine($"Beard Style: {GetBeardStyle(loadout)}");

		stringBuilder.AppendLine($"Torso Type: {GetTorsoType(loadout)}");
		stringBuilder.AppendLine($"Torso Style: {GetTorsoStyle(loadout)}");

		stringBuilder.AppendLine($"Legs Type: {GetLegsType(loadout)}");
		stringBuilder.AppendLine($"Legs Style: {GetLegsStyle(loadout)}");

		stringBuilder.AppendLine($"Has Boots: {HasBoots(loadout)}");
		stringBuilder.AppendLine($"Boots Style: {GetBootsStyle(loadout)}");

		stringBuilder.AppendLine($"Has Gloves: {HasGloves(loadout)}");
		stringBuilder.AppendLine($"Gloves Style: {GetGlovesStyle(loadout)}");

		return stringBuilder.ToString();
	}

	#endregion

	#region Encode
	public static uint CreateRandomLoadoutString() {
		bool hasHat = Random.Range(0, 2) == 1;
		byte hatStyleIndex = (byte)Random.Range(0, 10);

		bool hasBeard = Random.Range(0, 2) == 1;
		byte beardStyleIndex = (byte) Random.Range(0, 10);

		byte torsoTypeIndex = 3;//(byte) Random.Range(0, 4);
		byte torsoStyleIndex = (byte) Random.Range(0, 10);

		byte legsTypeIndex = 3;//(byte) Random.Range(0, 4);
		byte legsStyleIndex = (byte) Random.Range(0, 10);

		bool hasBoots = false;//Random.Range(0, 2) == 1;
		byte bootsStyleIndex = (byte) Random.Range(0, 8);

		bool hasGloves = false;//Random.Range(0, 2) == 1;
		byte glovesStyleIndex = (byte) Random.Range(0, 8);

		return CreateLoadoutString(hasHat, hatStyleIndex, hasBeard, beardStyleIndex, torsoTypeIndex, torsoStyleIndex, legsTypeIndex, legsStyleIndex, hasBoots, bootsStyleIndex, hasGloves, glovesStyleIndex);
	}

	public static uint CreateLoadoutString(bool hasHat, byte hatStyleIndex, bool hasBeard, byte beardStyleIndex, byte torsoTypeIndex, byte torsoStyleIndex, byte legsTypeIndex, byte legsStyleIndex, bool hasBoots, byte bootsStyleIndex, bool hasGloves, byte glovesStyleIndex) {
		uint ret = 0;

		if (hasHat) {
			ret |= (uint)NPCLoadoutElements.HAT;
			ret |= ((uint)hatStyleIndex) << 25;
		}

		if (hasBeard) {
			ret |= (uint) NPCLoadoutElements.BEARD;
			ret |= ((uint) beardStyleIndex) << 20;
		}

		{
			ret |= ((uint) torsoTypeIndex) << 18;
			ret |= ((uint) torsoStyleIndex) << 14;
		}

		{
			ret |= ((uint) legsTypeIndex) << 12;
			ret |= ((uint) legsStyleIndex) << 8;
		}

		{
			ret |= (uint) NPCLoadoutElements.BOOTS;
			ret |= ((uint) bootsStyleIndex) << 4;
		}

		{
			ret |= (uint) NPCLoadoutElements.GLOVES;
			ret |= ((uint) glovesStyleIndex);
		}

		return ret;
	}
	#endregion

	#region Modify
	public static uint ModifyHat(uint loadout, bool hasHat) {

		if (!hasHat) loadout &= ~((uint) NPCLoadoutElements.HAT);
		else loadout |= ((uint) NPCLoadoutElements.HAT);

		return loadout;
	}

	public static uint ModifyHatStyle(uint loadout, uint hatStyleIndex) {

		loadout &= ~((uint) NPCLoadoutElements.HAT_STYLE);

		loadout |= hatStyleIndex << 25;

		return loadout;
	}

	public static uint ModifyBeard(uint loadout, bool hasBeard) {

		if (!hasBeard) loadout &= ~((uint) NPCLoadoutElements.BEARD);
		else loadout |= ((uint) NPCLoadoutElements.BEARD);

		return loadout;
	}

	public static uint ModifyBeardStyle(uint loadout, uint beardStyleIndex) {

		loadout &= ~((uint) NPCLoadoutElements.BEARD_STYLE);

		loadout |= beardStyleIndex << 20;

		return loadout;
	}

	public static uint ModifyTorsoType(uint loadout, uint torsoTypeIndex) {
		loadout &= ~((uint) NPCLoadoutElements.TORSO);

		loadout |= torsoTypeIndex << 18;

		return loadout;
	}

	public static uint ModifyTorsoStyle(uint loadout, uint torsoStyleIndex) {
		loadout &= ~((uint) NPCLoadoutElements.TORSO_STYLE);

		loadout |= torsoStyleIndex << 14;

		return loadout;
	}

	public static uint ModifyLegsType(uint loadout, uint legsTypeIndex) {
		loadout &= ~((uint) NPCLoadoutElements.LEGS);

		loadout |= legsTypeIndex << 12;

		return loadout;
	}

	public static uint ModifyLegsStyle(uint loadout, uint legsStyleIndex) {
		loadout &= ~((uint) NPCLoadoutElements.LEGS_STYLE);

		loadout |= legsStyleIndex << 8;

		return loadout;
	}

	public static uint ModifyBoots(uint loadout, bool hasBoots) {
		if (!hasBoots) loadout &= ~((uint) NPCLoadoutElements.BOOTS);
		else loadout |= ((uint) NPCLoadoutElements.BOOTS);

		return loadout;
	}

	public static uint ModifyBootsStyle(uint loadout, uint bootsStyleIndex) {
		loadout &= ~((uint) NPCLoadoutElements.BOOTS_STYLE);

		loadout |= bootsStyleIndex << 4;

		return loadout;
	}

	public static uint ModifyGloves(uint loadout, bool hasGloves) {
		if (!hasGloves) loadout &= ~((uint) NPCLoadoutElements.GLOVES);
		else loadout |= ((uint) NPCLoadoutElements.GLOVES);

		return loadout;
	}

	public static uint ModifyGlovesStyle(uint loadout, uint glovesStyleIndex) {
		loadout &= ~((uint) NPCLoadoutElements.GLOVES_STYLE);

		loadout |= glovesStyleIndex;

		return loadout;
	}
#endregion

	public static uint GetMaskFromLoadoutElement(NPCLoadoutElements element) {
		return (uint) element;
	}

}

public enum NPCLoadoutElements : uint {

	HAT				= 0b1_0000_0_0000_00_0000_00_0000_0_000_0_000,
	HAT_STYLE		= 0b0_1111_0_0000_00_0000_00_0000_0_000_0_000,
	BEARD			= 0b0_0000_1_0000_00_0000_00_0000_0_000_0_000,
	BEARD_STYLE		= 0b0_0000_0_1111_00_0000_00_0000_0_000_0_000,
	TORSO			= 0b0_0000_0_0000_11_0000_00_0000_0_000_0_000,
	TORSO_STYLE		= 0b0_0000_0_0000_00_1111_00_0000_0_000_0_000,
	LEGS			= 0b0_0000_0_0000_00_0000_11_0000_0_000_0_000,
	LEGS_STYLE		= 0b0_0000_0_0000_00_0000_00_1111_0_000_0_000,
	BOOTS 			= 0b0_0000_0_0000_00_0000_00_0000_1_000_0_000,
	BOOTS_STYLE		= 0b0_0000_0_0000_00_0000_00_0000_0_111_0_000,
	GLOVES			= 0b0_0000_0_0000_00_0000_00_0000_0_000_1_000,
	GLOVES_STYLE	= 0b0_0000_0_0000_00_0000_00_0000_0_000_0_111

}


