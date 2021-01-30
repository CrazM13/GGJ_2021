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

		if (Random.Range(0f, 1f) > loyalty) GetLie(targetLoadout);
		else if (Random.Range(0f, 1f) > loyalty) GetGuess(targetLoadout);
		else GetTruth(targetLoadout);
	}

	public void GetLie(uint target) {
		int topic = Random.Range(0, 6);
		switch (topic) {
			case 0:

				uint colorHat;
				do {
					colorHat = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorHat == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorHat]} Hat..") buffer.Enqueue(c);

				break;
			case 1:

				uint colorBeard;
				do {
					colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorBeard == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorBeard]} Beard..") buffer.Enqueue(c);

				break;
			case 2:

				uint colorTorso;
				do {
					colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorTorso == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorTorso]} Torso..") buffer.Enqueue(c);

				break;
			case 3:

				uint colorLegs;
				do {
					colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);
				} while (colorLegs == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorLegs]} Pants..") buffer.Enqueue(c);

				break;
			case 4:

				uint colorBoots;
				do {
					colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorBoots == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {secondaryPalletNames[colorBoots]} Boots..") buffer.Enqueue(c);

				break;
			case 5:

				uint colorGloves;
				do {
					colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);
				} while (colorGloves == NPCLoadoutHelper.GetHatStyle(target));

				foreach (char c in $"Pretty sure I saw a {secondaryPalletNames[colorGloves]} Gloves..") buffer.Enqueue(c);

				break;
		}
	}

	public void GetGuess(uint target) {
		int topic = Random.Range(0, 6);
		switch (topic) {
			case 0:

				uint colorHat = (uint)Random.Range(0, primaryPalletNames.Length);

				foreach (char c in $"I think I saw a {primaryPalletNames[colorHat]} Hat..") buffer.Enqueue(c);

				break;
			case 1:

				uint colorBeard = (uint) Random.Range(0, primaryPalletNames.Length);

				foreach (char c in $"I think I saw a {primaryPalletNames[colorBeard]} Beard..") buffer.Enqueue(c);

				break;
			case 2:

				uint colorTorso = (uint) Random.Range(0, primaryPalletNames.Length);

				foreach (char c in $"I think I saw a {primaryPalletNames[colorTorso]} Torso..") buffer.Enqueue(c);

				break;
			case 3:

				uint colorLegs = (uint) Random.Range(0, primaryPalletNames.Length);

				foreach (char c in $"I think I saw a {primaryPalletNames[colorLegs]} Pants..") buffer.Enqueue(c);

				break;
			case 4:

				uint colorBoots = (uint) Random.Range(0, secondaryPalletNames.Length);

				foreach (char c in $"I think I saw a {secondaryPalletNames[colorBoots]} Boots..") buffer.Enqueue(c);

				break;
			case 5:

				uint colorGloves = (uint) Random.Range(0, secondaryPalletNames.Length);

				foreach (char c in $"I think I saw a {secondaryPalletNames[colorGloves]} Gloves..") buffer.Enqueue(c);

				break;
		}
	}

	public void GetTruth(uint target) {
		int topic = Random.Range(0, 6);
		switch (topic) {
			case 0:

				uint colorHat = NPCLoadoutHelper.GetHatStyle(target);

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorHat]} Hat..") buffer.Enqueue(c);

				break;
			case 1:

				uint colorBeard = NPCLoadoutHelper.GetBeardStyle(target);

				Debug.Log(colorBeard);
				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorBeard]} Beard..") buffer.Enqueue(c);

				break;
			case 2:

				uint colorTorso = NPCLoadoutHelper.GetTorsoStyle(target);

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorTorso]} Torso..") buffer.Enqueue(c);

				break;
			case 3:

				uint colorLegs = NPCLoadoutHelper.GetLegsStyle(target);

				foreach (char c in $"Pretty sure I saw a {primaryPalletNames[colorLegs]} Pants..") buffer.Enqueue(c);

				break;
			case 4:

				uint colorBoots = NPCLoadoutHelper.GetBootsStyle(target);

				foreach (char c in $"Pretty sure I saw a {secondaryPalletNames[colorBoots]} Boots..") buffer.Enqueue(c);

				break;
			case 5:

				uint colorGloves = NPCLoadoutHelper.GetGlovesStyle(target);

				foreach (char c in $"Pretty sure I saw a {secondaryPalletNames[colorGloves]} Gloves..") buffer.Enqueue(c);

				break;
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
