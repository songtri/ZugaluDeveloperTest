using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
	[SerializeField]
	private Text UserName = null;

	// Start is called before the first frame update
	void Start()
	{
		UserName.text = GlobalData.userName;
	}

	public void OnClickExit()
	{
		Application.Quit();
	}
}
