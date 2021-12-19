using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
	[SerializeField]
	private InputField UserNameInputField = null;
	[SerializeField]
	private GameObject ErrorPopup = null;

	private void Start()
	{
		ErrorPopup.SetActive(false);
	}

	public void OnClickStart()
	{
		if (string.IsNullOrEmpty(UserNameInputField.text) || string.IsNullOrWhiteSpace(UserNameInputField.text))
		{
			ErrorPopup.SetActive(true);
		}
		else
			SceneManager.LoadSceneAsync("02_GameScene");
	}

	public void OnClickExit()
	{
		Application.Quit();
	}

	public void OnClickClosePopup()
	{
		ErrorPopup.SetActive(false);
	}
}
