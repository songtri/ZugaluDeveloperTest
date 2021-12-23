using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
	public string Name;

	public float HitPoint;

	public float MoveSpeed;

	public float AttackRange;
	public float AttackSpeed;	// number of attacks per second
	public float AttackPower;

	public Stats Clone()
	{
		Stats newStats = new Stats();
		newStats.Name = Name;
		newStats.HitPoint = HitPoint;
		newStats.MoveSpeed = MoveSpeed;
		newStats.AttackRange = AttackRange;
		newStats.AttackSpeed = AttackSpeed;
		newStats.AttackPower = AttackPower;

		return newStats;
	}
}
