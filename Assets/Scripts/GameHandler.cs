using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	[SerializeField]
	private Camera MainCamera = null;
	[SerializeField]
	private GameUIHandler UiHandler = null;
	[SerializeField]
	private Transform MapParent = null;
	[SerializeField]
	private Transform UnitParent = null;

	private GameObject tempSelectedObject = null;
	private Actor selectedObject = null;
	private GameObject movePositionIndicator = null;

	public string[] mapData =
	{
		"1111111111111111111111111111111111111111",
		"1111111111111111111111111111111111111111",
		"1111111111111111111111111111111111111111",
		"1111111000000000001111111111111111111111",
		"1111111111111111111111111111111111111111",
		"1111111111111000000001111111111111111111",
		"1111111111111111111111111111111111111111",
		"1111111111000000001111111111111111111111",
		"1111111111111111111111111111111111111111",
		"1111111111111111111111111111011111111111",
		"1111111111111111111111111111011111111111",
		"1111111111111111111111111111011101111111",
		"1111111111111111111111111111011101111111",
		"1111111111111111111111111111011101111111",
		"1111111111111111111111111111011101111111",
		"1111111111111111111111111111011101111111",
		"1111111111111111111111111111111101111111",
		"1111111111111111111111111111111101111111",
		"1111111111111111111111111111111111111111",
		"1111111111111111111111111111111111111111",
	};
	public MapGrid MapGrid = null;

	public List<Actor> UnitList
	{
		get => unitList;
	}
	private List<Actor> unitList = new List<Actor>();

	// Start is called before the first frame update
	void Start()
	{
		SpawnMap();
		SpawnObjects();
	}

	// Update is called once per frame
	void Update()
	{
		// handle input
		if (Input.GetMouseButtonDown(0))
		{
			if (GetClickedObject(out RaycastHit hit))
			{
				tempSelectedObject = hit.collider.gameObject;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (tempSelectedObject != null && GetClickedObject(out RaycastHit hit))
			{
				if (tempSelectedObject == hit.collider.gameObject)
				{
					if (tempSelectedObject.CompareTag("map"))
					{
						if (selectedObject != null)
						{
							ShowMoveIndicator(hit.point);
							selectedObject.MoveTo(hit.point);
						}
					}
					else
					{
						var actor = tempSelectedObject.GetComponent<Actor>();
						if (actor != null)
						{
							actor.Selected();
							if (actor != selectedObject)
							{
								if (selectedObject != null)
									selectedObject.Deselected();
								selectedObject = actor;
							}
						}
					}
				}
			}
		}
	}

	private void SpawnMap()
	{
		MapGrid = new MapGrid();
		MapGrid.InitGrid(mapData.Length, mapData[0].Length);

		var mapObj = Resources.Load("Maps/DefaultMap") as GameObject;
		if (mapObj != null)
		{
			int mapSizeX = mapData.Length;
			for (int i = 0; i < mapSizeX; ++i)
			{
				int mapSizeY = mapData[i].Length;
				for (int j = 0; j < mapSizeY; ++j)
				{
					var go = Instantiate(mapObj, MapParent);
					go.transform.position = new Vector3(j - mapSizeY / 2, 0f, mapSizeX - i - mapSizeX / 2);
					bool isMovable = mapData[i][j] == '1';
					var mr = go.GetComponent<MeshRenderer>();
					if (mr != null && mr.materials != null && mr.materials.Length > 0)
					{
						if (isMovable)
							mr.materials[0].color = Color.yellow;
						else
							mr.materials[0].color = Color.red;
					}
					var node = go.AddComponent<MapNode>();
					node.SetData(isMovable, i, j, go.transform.position);
					MapGrid.AddNode(node, i, j);
				}
			}
		}
	}

	private void SpawnObjects()
	{
		Vector3[] unitsPos = new Vector3[] {
			new Vector3(-1, 1, -1),
			new Vector3(-1, 1, 1),
			new Vector3(1, 1, -1),
			new Vector3(1, 1, 1)
		};

		for (int i = 0; i < unitsPos.Length; ++i)
		{
			var unit = UnitFactory.CreateUnit(0, UnitParent, unitsPos[i]);
			if (unit != null)
			{
				unit.gameHandler = this;
				unitList.Add(unit);
			}
		}

		var building = UnitFactory.CreateBuilding(0, UnitParent, new Vector3(10, 2, 10));
		if (building != null)
		{
			building.gameHandler = this;
			unitList.Add(building);
		}
	}

	private bool GetClickedObject(out RaycastHit hitInfo)
	{
		Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			return true;
		}

		hitInfo = default;
		return false;
	}

	private void ShowMoveIndicator(Vector3 pos)
	{
		movePositionIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		movePositionIndicator.transform.localPosition = pos;
		movePositionIndicator.AddComponent<MoveToIndicator>();
	}
}
