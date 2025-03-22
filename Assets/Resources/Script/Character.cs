using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AtractCharacter
{
    public TargetIndicator indicator;
    private string currentAnim;
    public Animator animator;
    public CharacterRange range;
    public Bullet bulletPrefabs;
    public bool isAttack=false;
    public int level = 1;
    public bool isDead=false;
    public InitSkin skin;
    public Transform indicatorHead;

    void Start()
    {
        SetBodyScale();
    }

    public void SetBodyScale()
    {
        transform.localScale = (1+(level-1)*0.15f)*Vector3.one;
    }

    public override void GainLevel()
    {
        if (!isDead)
        {
            level++;
            SetBodyScale();
            indicator.InitTarget(level);
        }
    }

    public void Throw()
    {
        range.RemoveNullTarget();

        if (range.botInRange.Count > 0)
        {
            Transform target = range.GetNearestTarget();
            if (target == null) return;

            skin.weaponItem.SetActive(false);
            Bullet bullet = Instantiate(bulletPrefabs);
            float scaleMultiplier = 1 + level * 0.1f;
            scaleMultiplier = Mathf.Clamp(scaleMultiplier, 1f, 3f);
            bullet.transform.localScale = Vector3.one * scaleMultiplier;
            bullet.transform.position = transform.position + Vector3.up * 1f;
            bullet.self = this;
            Vector3 direction = (target.position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            transform.forward = direction;
            StartCoroutine(RotateWeapons(bullet));
            Invoke(nameof(EnableWeapons), 0.5f);
        }
    }



    private IEnumerator RotateWeapons(Bullet bullets)
    {
        while (bullets != null && bullets.gameObject != null)
        {
            bullets.transform.Rotate(0, 360f * Time.deltaTime,0);
            yield return null;
        }
    }

    private void EnableWeapons()
    {
        skin.weaponItem.SetActive(true);
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }


    public override void OnInit()
    {
        
    }

    public override void OnAttack()
    {
        Throw();
    }



    public override void OnDead()
    {
        isDead = true;
        ChangeAnim("dead");
        gameObject.tag = "Untagged";
        
    }
}
