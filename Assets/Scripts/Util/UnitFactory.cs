using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitFactory
{
	public static Actor CreateUnit(int id, Transform parent, Vector3 initialPos)
	{
		string prefabName;
		switch (id)
		{
			case 0:
				prefabName = "DefaultUnit";
				break;
			default:
				prefabName = "DefaultUnit";
				break;
		}

		var go = Object.Instantiate(Resources.Load($"Units/Characters/{prefabName}"), parent) as GameObject;
		if (go != null)
		{
			go.transform.localPosition = initialPos;
			return go.GetComponent<Actor>();
		}

		return null;
	}

	public static Actor CreateBuilding(int id, Transform parent, Vector3 initialPos)
	{
		string prefabName;
		switch (id)
		{
			case 0:
				prefabName = "Turret";
				break;
			default:
				prefabName = "Turret";
				break;
		}

		var go = Object.Instantiate(Resources.Load($"Units/Buildings/{prefabName}"), parent) as GameObject;
		if (go != null)
		{
			go.transform.localPosition = initialPos;
			return go.GetComponent<Actor>();
		}

		return null;
	}
}
