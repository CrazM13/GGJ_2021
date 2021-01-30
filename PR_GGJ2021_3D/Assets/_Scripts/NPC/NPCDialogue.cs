using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour {

	[SerializeField] private NPCManager manager;
	[SerializeField] private PlayerController player;

	[SerializeField] private CanvasGroup textbox;
	[SerializeField] private Text text;

	private readonly string[] primaryPalletNames = new string[] {
		"Red",
		"Orange",
		"Yellow",
		"Green",
		"Pink",
		"Blue",
		"Black",
		"Purple",
		"White",
		"Grey"
	};

	private readonly string[] secondaryPalletNames = new string[] {
		"Orange",
		"Blue",
		"Maroon",
		"Red",
		"Green",
		"Pink",
		"White",
		"Black"
	};

	Queue<char> buffer = new Queue<char>();

	public void GetDialogue(NPCBase npc) {
		player.IsInTextBox = true;

		textbox.alpha = 1;
		textbox.interactable = true;
		textbox.blocksRaycasts = true;

		float loyalty = npc.GetLoyalty();

		uint targetLoadout = manager.GetTargetLoadout();

		if (Random.Range(0f, 1f) > loyalty) GetLie(npc, targetLoadout);
		else if (Random.Range(0f, 1f) > loyalty) GetGuess(npc, targetLoadout);
		else GetTruth(npc, targetLoadout);
	}

	public void SetDialogue(string lines) {
		player.IsInTextBox = true;

		textbox.alpha = 1;
		textbox.interactable = true;
		textbox.blocksRaycasts = true;

		foreach (char c in lines) buffer.Enqueue(c);
	}

	public void GetLie(NPCBase npc, uint target) {
		int topic = Random.Range(0, 6);
		string selected = "";

		switch (topic) {
			case 0:

				uint colorHat;
				do {
					colorHat = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorHat == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard;
				do {
					colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorBeard == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso;
				do {
					colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorTorso == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {primaryPalletNames[colorTorso]} Torso..";

				break;
			case 3:

				uint colorLegs;
				do {
					colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorLegs == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots;
				do {
					colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorBoots == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves;
				do {
					colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorGloves == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"Pretty sure I saw a {secondaryPalletNames[colorGloves]} Gloves..";

				break;
		}

		if (selected.Length > 0) {
			foreach (char c in selected) buffer.Enqueue(c);
			npc.StoreDialogue(selected);
		}
	}

	public void GetGuess(NPCBase npc, uint target) {
		int topic = Random.Range(0, 6);

		string selected = "";

		switch (topic) {
			case 0:

				uint colorHat = (uint)Random.Range(0, primaryPalletNames.Length);

				selected = $"I think I saw a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"I think I saw a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"I think I saw a {primaryPalletNames[colorTorso]} Torso..";

				break;
			case 3:

				uint colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"I think I saw a {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);

				selected = $"I think I saw a {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);

				selected = $"I think I saw a {secondaryPalletNames[colorGloves]} Gloves..";

				break;
		}



		if (selected.Length > 0) {
			foreach (char c in selected) buffer.Enqueue(c);
			npc.StoreDialogue(selected);
		}
	}

	public void GetTruth(NPCBase npc, uint target) {
		int topic = Random.Range(0, 6);

		string selected = "";

		switch (topic) {
			case 0:

				uint colorHat = NPCLoadoutHelper.GetHatStyle(target);

				Debug.Log(colorHat);
				selected = $"Pretty sure I saw a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard = NPCLoadoutHelper.GetBeardStyle(target);

				Debug.Log(colorBeard);
				selected = $"Pretty sure I saw a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso = NPCLoadoutHelper.GetTorsoStyle(target);

				selected = $"Pretty sure I saw a {primaryPalletNames[colorTorso]} Torso..";

				break;
			case 3:

				uint colorLegs = NPCLoadoutHelper.GetLegsStyle(target);

				Debug.Log(colorLegs);
				selected = $"Pretty sure I saw a {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots = NPCLoadoutHelper.GetBootsStyle(target);

				Debug.Log(colorBoots);
				selected = $"Pretty sure I saw a {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves = NPCLoadoutHelper.GetGlovesStyle(target);

				Debug.Log(colorGloves);
				selected = $"Pretty sure I saw a {secondaryPalletNames[colorGloves]} Gloves..";

				break;
		}

		if (selected.Length > 0) {
			foreach (char c in selected) buffer.Enqueue(c);
			npc.StoreDialogue(selected);
		}
	}

	void Update() {
		if (!textbox.interactable) return;

		if (buffer.Count > 0) text.text += buffer.Dequeue();

		if (Input.anyKeyDown) {
			if (buffer.Count > 0) {
				for (int i = buffer.Count; i > 0; i--) {
					text.text += buffer.Dequeue();
				}
			} else {
				text.text = "";

				textbox.alpha = 0;
				textbox.interactable = false;
				textbox.blocksRaycasts = false;

				player.IsInTextBox = false;
			}
		}
	}
}
