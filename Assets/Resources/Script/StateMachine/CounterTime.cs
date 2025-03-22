using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CouterTime
{
    UnityAction playerAction;
    private float time;
    public void Start(UnityAction playerAction, float time)
    {
        this.playerAction = playerAction;
        this.time = time;
    }

    public void Execute()
    {
        if(time >0)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                Exit();
            }
        }
    }
    public void Exit()
    {
        playerAction?.Invoke();
    }
    public void Cancel()
    {
        playerAction = null;
    }


}
