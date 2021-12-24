using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
	public bool isMovable;
	public int x;
	public int y;
	public Vector3 worldPos;
	public int gCost;
	public int hCost;
	public int fCost
	{
		get => gCost + hCost;
	}
	public MapNode prevNode = null;

	public static float sizeX = 1f;
	public static float sizeY = 1f;

	public MapNode()
	{
		gCost = 0;
		hCost = 0;
		prevNode = null;
	}

	public void SetData(bool isMovable, int x, int y, Vector3 worldPos)
	{
		this.isMovable = isMovable;
		this.x = x;
		this.y = y;
		this.worldPos = worldPos;
		gCost = 0;
		hCost = 0;

		gameObject.name = $"Node_{x.ToString("D2")}_{y.ToString("D2")}";
	}
}
