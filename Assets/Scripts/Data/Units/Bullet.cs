using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Vector3 targetPos;
	private const float speed = 10f;

	public void InitPos(Vector3 pos, Vector3 targetPos)
	{
		transform.position = pos;
		this.targetPos = targetPos;
	}

	private void Start()
	{
		transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

		var renderer = GetComponent<MeshRenderer>();
		if (renderer != null && renderer.materials.Length > 0)
		{
			renderer.materials[0].color = Color.red;
		}
	}

	private void Update()
	{
		var moveDir = targetPos - transform.position;
		moveDir.y = 0f;

		if (moveDir.sqrMagnitude < 0.001f)
			Destroy(gameObject);

		moveDir.Normalize();
		transform.position = transform.position + moveDir * speed * Time.deltaTime;
	}
}
