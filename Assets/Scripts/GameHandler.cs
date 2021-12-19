using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
	[SerializeField]
	private Camera mainCamera = null;
	[SerializeField]
	private GameUIHandler uiHandler = null;

	private GameObject tempSelectedObject = null;
	private Actor selectedObject = null;

	private GameObject map = null;
	private List<Actor> unitList = new List<Actor>();

	// Start is called before the first frame update
	void Start()
	{
		var actors = FindObjectsOfType<Actor>();
		unitList.AddRange(actors);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			tempSelectedObject = GetClickedObject();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (tempSelectedObject != null && tempSelectedObject == GetClickedObject())
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

	private void SpawnObjects()
	{
	}

	private GameObject GetClickedObject()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			return hit.collider.gameObject;
		}

		return null;
	}
}
