using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        bot.MoveToRandomPoint(bot);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination)
        {
            bot.ChangeState(new IdleState());
            return;
        }
        Transform nearestEnemy = bot.FindNearestEnemy(bot);
        if (nearestEnemy != null)
        {
            Vector3 directionToEnemy = (nearestEnemy.position - bot.transform.position).normalized;
            Vector3 targetPosition = nearestEnemy.position - directionToEnemy * 3f;

            bot.SetDestination(targetPosition);
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
