using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Actor
{
	[SerializeField]
	protected Stats InitialStat = null;

	protected Stats CurrentStat = null;

	private float attackTimer = 0f;

	protected override void Start()
	{
		base.Start();

		CurrentStat = InitialStat.Clone();
	}

	protected override void Update()
	{
		base.Update();

		if (attackTimer == 0f)
		{
			foreach (var target in gameHandler.UnitList)
			{
				if (target == this)
					continue;

				var diffVec = target.transform.position - transform.position;
				var distSq = diffVec.sqrMagnitude;
				if (distSq < CurrentStat.AttackRange * CurrentStat.AttackRange)
				{
					OnAttack(target);
					target.OnAttacked(this);
				}
			}
		}
		else
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0f)
				attackTimer = 0f;
		}
	}

	public override void OnAttack(Actor target)
	{
		base.OnAttack(target);

		attackTimer = 1f / CurrentStat.AttackSpeed;

		var obj  = GameObject.CreatePrimitive(PrimitiveType.Cube);
		var bullet = obj.AddComponent<Bullet>();
		bullet.InitPos(transform.position, target.transform.position);
	}
}
