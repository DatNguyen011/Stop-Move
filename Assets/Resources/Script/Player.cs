using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector3 nextPoints;
    public LayerMask groundLayer;
    private CouterTime couterTime = new CouterTime();

    void Update()
    {
        nextPoints = transform.position + JoystickControl.direct * Time.deltaTime * 5f;
        if (CheckGround(nextPoints) && JoystickControl.direct.magnitude > 0f)
        {
            couterTime.Cancel();
            transform.position = nextPoints;
            transform.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else if (!isAttack)
        {
            couterTime.Execute();
            range.RemoveNullTarget();
            ChangeAnim("idle");
            if (range.botInRange.Count > 0)
            {
                AttackTarget();
            }
        }
        else
        {
            couterTime.Execute();
        }
    }

    public void AttackTarget()
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnim("attack");
        couterTime.Start(Throw, 0.5f);
    }

    private void ChangeIsAttack()
    {
        isAttack = false;
    }

    private bool CheckGround(Vector3 points)
    {
        RaycastHit hit;
        return Physics.Raycast(points + Vector3.up * 2, Vector3.down, out hit, 3f, groundLayer);
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDead()
    {
        base.OnDead();
        foreach (CharacterRange range in FindObjectsOfType<CharacterRange>())
        {
            range.botInRange.Remove(this);
        }
        couterTime.Cancel();
        this.enabled = false;
        GameController.Instance.GameOver();
    }

    public override void OnInit()
    {
        base.OnInit();
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
        skin.PlayerEquipItems();
        bulletPrefabs = ItemData.Instance.bullets[skin.weaponId];
        this.enabled = true;
        isDead = false;
        gameObject.tag = "Bot";
        ChangeAnim("idle");
        indicator.InitTarget(UnityEngine.Color.green, "player", 1);
        indicator.gameObject.SetActive(true);
        transform.rotation = Quaternion.identity;
    }

    public override void GainLevel()
    {
        base.GainLevel();
        CameraFollower.Instance.UpdateCameraOffset(level);
    }

    public void UpdateWeapons()
    {
        skin.PlayerEquipItems();
        bulletPrefabs = ItemData.Instance.bullets[skin.weaponId];
    }

    public void OnDespawn()
    {
        couterTime.Cancel();
    }

}
