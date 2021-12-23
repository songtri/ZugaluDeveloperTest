using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Actor
{
	[SerializeField]
	private Stats InitialStat = null;

	private Stats CurrentStat = null;

	protected override void Start()
	{
		base.Start();

		CurrentStat = InitialStat.Clone();
	}

	protected override void Update()
	{
		base.Update();

		if (isMoving)
		{
			var moveDir = moveToPosition - transform.localPosition;
			moveDir.y = 0f;
			moveDir.Normalize();
			transform.localPosition = transform.localPosition + moveDir * CurrentStat.MoveSpeed * Time.deltaTime;
		}
	}
}
