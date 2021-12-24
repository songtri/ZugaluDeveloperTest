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
		var moveDir = intermediateMoveToPosition - transform.position;

		if (moveDir.sqrMagnitude < 0.01f)
		{
			transform.position = intermediateMoveToPosition;
			++currentMoveIndex;
			if (currentMoveIndex >= movePath.Count)
				isMoving = false;
			else
			{
				intermediateMoveToPosition = movePath[currentMoveIndex].worldPos;
				intermediateMoveToPosition.y = transform.position.y;
			}
		}
		else
		{
			moveDir.Normalize();
			transform.position += moveDir * CurrentStat.MoveSpeed * Time.deltaTime;
		}
	}
}
