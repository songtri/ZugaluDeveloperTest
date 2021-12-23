using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToIndicator : MonoBehaviour
{
	private const float TimeToLive = 2f;
	private float timer = 0f;

	private void Start()
	{
		gameObject.name = "MoveIndicator";
		timer = TimeToLive;
		transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

		var renderer = GetComponent<MeshRenderer>();
		if (renderer != null && renderer.materials.Length > 0)
		{
			renderer.materials[0].color = Color.green;
		}
	}

	private void Update()
	{
		if (timer > 0f)
			timer -= Time.deltaTime;
		else
			Destroy(gameObject);
	}

}
