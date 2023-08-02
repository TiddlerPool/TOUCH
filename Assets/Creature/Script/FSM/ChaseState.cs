using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 1;
        if(enemy.TargetList.Count != 0)
        enemy.CurrentTarget = enemy.TargetList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        if(enemy.E2TDistance() >= enemy.AttackRange)
        {
            enemy.EnemyMove();
        }
        else
        {
            enemy.TransitionToState(enemy.AttackState);
        }

        if(enemy.Territorial)
        {
            if ((enemy.transform.position - enemy.Territory.position).magnitude > enemy.TerritoryRange)
            {
                enemy.CurrentTarget = null;
                enemy.TargetList = null;
                enemy.TransitionToState(enemy.PatrolState);
            }
        }
    }
}
