using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[SerializeField]
	private Color DefaultColor = Color.green;
	[SerializeField]
	private Color SelectedColor = Color.blue;

	protected Material defaultMat;

	public Action onSelectedHandler;
	public Action onDeselectHandler;

	protected Vector3 moveToPosition;
	protected bool isMoving = false;

	public GameHandler gameHandler = null;

	protected virtual void Start()
	{
		var renderer = GetComponent<MeshRenderer>();
		if (renderer != null)
		{
			var materials = renderer.materials;
			if (materials.Length > 0)
			{
				defaultMat = materials[0];
				defaultMat.color = DefaultColor;
			}
		}

		isMoving = false;
	}

	protected virtual void Update()
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

	public void MoveTo(Vector3 destPos)
	{
		moveToPosition = destPos;
		isMoving = true;
	}

	public virtual void OnAttack(Actor target)
	{
	}

	public virtual void OnAttacked(Actor attacker)
	{
	}

	protected virtual void move()
	{
	}
}
