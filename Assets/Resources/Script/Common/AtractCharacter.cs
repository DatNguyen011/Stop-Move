using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtractCharacter : MonoBehaviour
{
    public abstract void OnInit();
    public abstract void OnAttack();
    public abstract void OnDead();

    public abstract void GainLevel();
}

   

