using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 2;
        enemy.anim.SetLayerWeight(1, 1f);
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (enemy.TargetList.Count == 0)
        {
            enemy.TransitionToState(enemy.PatrolState);
        }

        if (enemy.TargetList.Count > 1)
        {
            for (int i = 0; i < enemy.TargetList.Count; i++)
            {
                if (Vector3.Distance(enemy.transform.position, enemy.TargetList[i].position) <
                    enemy.E2TDistance())
                {
                    enemy.CurrentTarget = enemy.TargetList[i];
                }
            }
        }
        if (enemy.TargetList.Count == 1)
        {
            enemy.CurrentTarget = enemy.TargetList[0];
        }

        if (enemy.CurrentTarget != null)
        {
            enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation,
            Quaternion.LookRotation(enemy.E2TDirection()),
            Time.deltaTime * 2f);
            if (enemy.E2TDistance() <= enemy.AttackRange)
            {
                enemy.AttackAction();
            }
            else
            {
                enemy.TransitionToState(enemy.ChaseState);
            }
        }
        else
        {
            enemy.TransitionToState(enemy.PatrolState);
        } 
    }
}
