using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour {

	[SerializeField] private NPCManager manager;
	[SerializeField] private PlayerController player;

	[SerializeField] private CanvasGroup textbox;
	[SerializeField] private Text text;

	private const string strLie = "Yeah there was";
	private const string strGuess = "I think I saw";
	private const string strTruth = "I'm pretty sure there were";

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

				selected = $"{strLie} a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard;
				do {
					colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorBeard == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"{strLie} a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso;
				do {
					colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorTorso == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"{strLie} a {primaryPalletNames[colorTorso]} Shirt..";

				break;
			case 3:

				uint colorLegs;
				do {
					colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorLegs == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"{strLie} {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots;
				do {
					colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorBoots == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"{strLie} {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves;
				do {
					colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorGloves == NPCLoadoutHelper.GetHatStyle(target));

				selected = $"{strLie} {secondaryPalletNames[colorGloves]} Gloves..";

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

				selected = $"{strGuess} a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"{strGuess} a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"{strGuess} a {primaryPalletNames[colorTorso]} Shirt..";

				break;
			case 3:

				uint colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);

				selected = $"{strGuess} {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);

				selected = $"{strGuess} {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);

				selected = $"{strGuess} {secondaryPalletNames[colorGloves]} Gloves..";

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
				selected = $"{strTruth} a {primaryPalletNames[colorHat]} Hat..";

				break;
			case 1:

				uint colorBeard = NPCLoadoutHelper.GetBeardStyle(target);

				Debug.Log(colorBeard);
				selected = $"{strTruth} a {primaryPalletNames[colorBeard]} Beard..";

				break;
			case 2:

				uint colorTorso = NPCLoadoutHelper.GetTorsoStyle(target);

				selected = $"{strTruth} a {primaryPalletNames[colorTorso]} Shirt..";

				break;
			case 3:

				uint colorLegs = NPCLoadoutHelper.GetLegsStyle(target);

				selected = $"{strTruth} {primaryPalletNames[colorLegs]} Pants..";

				break;
			case 4:

				uint colorBoots = NPCLoadoutHelper.GetBootsStyle(target);

				selected = $"{strTruth} {secondaryPalletNames[colorBoots]} Boots..";

				break;
			case 5:

				uint colorGloves = NPCLoadoutHelper.GetGlovesStyle(target);

				selected = $"{strTruth} {secondaryPalletNames[colorGloves]} Gloves..";

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
