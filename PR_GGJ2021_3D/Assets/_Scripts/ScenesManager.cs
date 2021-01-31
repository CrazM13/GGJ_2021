using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	public static ScenesManager instance;



	void Awake() {
		if (!instance) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void MainMenu() {
		SceneManager.LoadScene("MenuScene");
		OnSceneChange();
	}

	public void Play() {
		SceneManager.LoadScene("GameScene");
		OnSceneChange();
	}

	public void Win() {
		SceneManager.LoadScene("WinScene");
		OnSceneChange();
	}

	public void Lose() {
		SceneManager.LoadScene("LoseScene");
		OnSceneChange();
	}

	public void Quit() {
		OnSceneChange();
		Application.Quit();
	}

	private void OnSceneChange() {
		Cursor.lockState = CursorLockMode.None;
	}

}
