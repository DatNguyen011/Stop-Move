using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public float timer = 0;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("attack");
        bot.isAttack = true;
        bot.ChangeIsAttackBot();
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > 0.5f) {
            if(bot.range.botInRange.Count > 0 )
            {
                bot.Throw();
            }
            bot.range.RemoveNullTarget();
            bot.ChangeState(new IdleState());
        }

    }

    public void OnExit(Bot bot)
    {
       
    }
}
