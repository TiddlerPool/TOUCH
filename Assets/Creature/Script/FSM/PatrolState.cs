using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;
        enemy.anim.SetLayerWeight(1, 0f);
        if(enemy.Patrolable)
        {
            enemy.CurrentTarget = enemy.PatrolSteps[enemy.stepIndex];
        }
        enemy.SwitchPatrolTargetPoints();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;
            enemy.EnemyMove();
        }
            enemy.SwitchPatrolTargetPoints();

        if (enemy.TargetList.Count > 0)
        {
            Debug.Log("State Transite Success");
            enemy.TransitionToState(enemy.ChaseState);
        }
    }

}
