using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	private Vector3 position;
	private Material defaultMat;

	private readonly Color DefaultColor = Color.green;
	private readonly Color SelectedColor = Color.blue;

	public Action onSelectedHandler;
	public Action onDeselectHandler;

	private void Start()
	{
		var materials = GetComponent<MeshRenderer>()?.materials;
		if (materials.Length > 0)
		{
			defaultMat = materials[0];
			defaultMat.color = DefaultColor;
		}
	}

	private void Update()
	{
		
	}

	public void Selected()
	{
		defaultMat.color = SelectedColor;
		onSelectedHandler?.Invoke();
	}

	public void Deselected()
	{
		defaultMat.color = DefaultColor;
		onDeselectHandler?.Invoke();
	}

	public void MoveTo(Vector2 destPos)
	{
	}

	public void Attack()
	{
	}

	public void move()
	{
	}
}
