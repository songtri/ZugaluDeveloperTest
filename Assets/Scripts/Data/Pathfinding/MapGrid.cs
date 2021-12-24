using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{
	private MapNode[,] nodes;
	private int sizeX = 0;
	private int sizeY = 0;

	public void InitGrid(int sizeX, int sizeY)
	{
		nodes = new MapNode[sizeX, sizeY];
		this.sizeX = sizeX;
		this.sizeY = sizeY;
	}

	public void AddNode(MapNode node, int x, int y)
	{
		nodes[x, y] = node;
	}

	public MapNode FindNodeByWorldPos(Vector3 pos)
	{
		for(int i=0; i<sizeX; ++i)
		{
			for (int j = 0; j < sizeY; ++j)
			{
				float xMin = nodes[i, j].worldPos.x - MapNode.sizeX / 2;
				float xMax = nodes[i, j].worldPos.x + MapNode.sizeX / 2;
				float yMin = nodes[i, j].worldPos.z - MapNode.sizeY / 2;
				float yMax = nodes[i, j].worldPos.z + MapNode.sizeY / 2;
				if (pos.x >= xMin && pos.x < xMax && pos.z >= yMin && pos.z < yMax)
					return nodes[i, j];
			}
		}

		return null;
	}

	public List<MapNode> GetNeighbours(MapNode node)
	{
		List<MapNode> neighbours = new List<MapNode>();
		for(int i = -1; i<=1;++i)
		{
			for (int j = -1; j <= 1; ++j)
			{
				if (i == 0 && j == 0)
					continue;

				int currentX = node.x + i;
				int currentY = node.y + j;

				if (currentX >= 0 && currentX < sizeX && currentY >= 0 && currentY < sizeY)
					neighbours.Add(nodes[currentX, currentY]);
			}
		}

		return neighbours;
	}

	public List<MapNode> FindPath(int startX, int startY, int destX, int destY)
	{
		MapNode startNode = nodes[startX, startY];
		MapNode destNode = nodes[destX, destY];

		List<MapNode> openList = new List<MapNode>();
		HashSet<MapNode> closedList = new HashSet<MapNode>();
		openList.Add(startNode);

		while (openList.Count > 0)
		{
			MapNode curNode = openList[0];
			for (int i = 1; i < openList.Count; ++i)
			{
				var searchNode = openList[i];
				if (searchNode.fCost < curNode.fCost || (searchNode.fCost == curNode.fCost && searchNode.hCost < curNode.hCost))
					curNode = openList[i];
			}

			openList.Remove(curNode);
			closedList.Add(curNode);

			if (curNode == destNode)
			{
				break;
			}

			foreach (var node in GetNeighbours(curNode))
			{
				if (node.isMovable == false || closedList.Contains(node))
					continue;

				int newCurToNeighboursCost = curNode.gCost + GetDistanceCost(curNode, node);
				if (newCurToNeighboursCost < node.gCost || !openList.Contains(node))
				{
					node.gCost = newCurToNeighboursCost;
					node.hCost = GetDistanceCost(node, destNode);
					node.prevNode = curNode;

					if (!openList.Contains(node))
						openList.Add(node);
				}
			}
		}

		return RetracePath(startNode, destNode);
	}

	public List<MapNode> RetracePath(MapNode start, MapNode dest)
	{
		List<MapNode> path = new List<MapNode>();
		MapNode curNode = dest;

		while(curNode != start)
		{
			path.Add(curNode);
			curNode = curNode.prevNode;

			var movePositionIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			movePositionIndicator.transform.localPosition = curNode.worldPos;
			movePositionIndicator.AddComponent<MoveToIndicator>();
		}

		path.Reverse();
		return path;
	}

	public int GetDistanceCost(MapNode from, MapNode to)
	{
		int distX = Mathf.Abs(from.x - to.x);
		int distY = Mathf.Abs(from.y - to.y);

		if (distX > distY)
			return 14 * distY + 10 * (distX - distY);
		else
			return 14 * distX + 10 * (distY - distX);
	}
}
