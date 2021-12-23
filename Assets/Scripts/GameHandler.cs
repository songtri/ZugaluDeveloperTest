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
	private GameObject Map = null;
	[SerializeField]
	private Transform UnitParent = null;

	private GameObject tempSelectedObject = null;
	private Actor selectedObject = null;
	private GameObject movePositionIndicator = null;

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
					if (tempSelectedObject.tag == "map")
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
		var mapObj = GameObject.Find("DefaultMap");
		if (mapObj != null)
		{
			Map = mapObj;
			var mr = Map.GetComponent<MeshRenderer>();
			if(mr.materials != null && mr.materials.Length > 0)
			{
				mr.materials[0].color = Color.yellow;
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
				unitList.Add(unit);
		}

		var building = UnitFactory.CreateBuilding(0, UnitParent, new Vector3(10, 2, 10));
		if (building != null)
			unitList.Add(building);
	}

	private bool GetClickedObject(out RaycastHit hitInfo)
	{
		Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			return true;
		}

		hitInfo = default(RaycastHit);
		return false;
	}

	private void ShowMoveIndicator(Vector3 pos)
	{
		movePositionIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		movePositionIndicator.transform.localPosition = pos;
		movePositionIndicator.AddComponent<MoveToIndicator>();
	}
}
