using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;
    public IState<Bot> currentState;

    public bool isDestination => Vector3.Distance
        (destination, Vector3.right*transform.position.x + Vector3.forward*transform.position.z ) < 0.1f;

    void Start()
    {
        ChangeAnim("idle");
    }

    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
          
    }

    public void ChangeIsAttackBot()
    {
        Invoke(nameof(ResetAttack),3f);
    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void MoveToRandomPoint(Bot bot)
    {
        Vector3 randomPoint;
        if (RandomPointOnNavMesh.Instance.RandomPoint(bot.transform.position, RandomPointOnNavMesh.Instance.range, out randomPoint))
        {
            bot.SetDestination(randomPoint);
        }
        else
        {
            bot.ChangeState(new IdleState());
        }
    }

    public Transform FindNearestEnemy(Bot bot)
    {
        Collider[] colliders = Physics.OverlapSphere(bot.transform.position, 15f);
        Transform nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Bot") && col.transform != bot.transform)
            {
                float distance = Vector3.Distance(bot.transform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = col.transform;
                }
            }
        }

        return nearestEnemy;
    }
    public void SetDestination(Vector3 des)
    {
        agent.enabled = true;
        destination = des;
        agent.SetDestination(des);
        destination.y = 0f;
    }

    public override void OnInit()
    {
        base.OnInit();
        level = Random.Range(1,4);
        SetBodyScale();
        indicator.InitTarget(level);
        if (!agent.enabled)
        {
            agent.enabled = true;
        }
        agent.ResetPath();
        ChangeState (new IdleState());
        skin.RandomSkinBot();
        bulletPrefabs = ItemData.Instance.bullets[skin.weaponId];
    }

    public override void OnAttack()
    {
        
        base.OnAttack();
    }

    public override void OnDead()
    {
        base.OnDead();
        ChangeState(null);
        foreach (CharacterRange range in FindObjectsOfType<CharacterRange>())
        {
            range.botInRange.Remove(this);
        }
        gameObject.tag = "Untagged";
        StartCoroutine(DestroyBot());
    }

    IEnumerator DestroyBot()
    {
        yield return new WaitForSeconds(3f);
        GameController.Instance.bots.Remove(this);
        indicator.gameObject.SetActive(false);
        gameObject.SetActive(false);
        skin.ResetSkinAndWeapon();
        GameController.Instance.ResponsNewBot();
    }

}
