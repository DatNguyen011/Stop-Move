using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Singleton<Bullet>
{
    public Character self;
    public Bot bot;

    void Start()
    {
        Destroy(gameObject, 1f + 0.05f * self.level);
    }

    

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Bot") && other.GetComponent<Character>() != self)
        {          
            other.GetComponent<Character>().OnDead();
            self.GainLevel();
            Destroy(gameObject);
        }
    }
}
