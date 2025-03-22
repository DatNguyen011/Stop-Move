using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    public List<Character> botInRange = new List<Character>();
    public GameObject targetBot;
    // Start is called before the first frame update

    public void RemoveNullTarget()
    {
        for (int i = botInRange.Count - 1; i >= 0; i--) 
        {
            if (botInRange[i] == null || !botInRange[i].CompareTag("Bot")) 
            {
                botInRange.RemoveAt(i);
            }
        }
    }

    public Transform GetNearestTarget()
    {
        float distanceMin = float.MaxValue;
        int index = 0;
        for (int i = 0; i < botInRange.Count; i++)
        {
            float distance = (transform.position - botInRange[i].transform.position).magnitude;
            if (distanceMin > distance)
            {
                distanceMin = distance;
                index = i;
            }
        }
        return botInRange[index].transform;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot")) 
        {
            Character botAdd = other.GetComponent<Character>();
            botInRange.Add(botAdd);
            targetBot.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            Character botToRemove = other.GetComponent<Character>();
            botInRange.Remove(botToRemove);
            targetBot.SetActive(false);
        }
    }

}
