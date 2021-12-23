using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Actor
{
	[SerializeField]
	protected Stats InitialStat = null;

	protected Stats CurrentStat = null;

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
			move();
		}
	}

	public override void OnAttacked(Actor attacker)
	{
		base.OnAttacked(attacker);
	}

	protected override void move()
	{
		moveToPosition.y = transform.position.y;
		var moveDir = moveToPosition - transform.position;
		moveDir.y = 0f;

		if (moveDir.sqrMagnitude < 0.001f)
		{
			transform.position = moveToPosition;
			isMoving = false;
		}
		else
		{
			moveDir.Normalize();
			transform.position += moveDir * CurrentStat.MoveSpeed * Time.deltaTime;
		}
	}
}
