using System;
using UnityEngine;

public class FinishGate : MonoBehaviour
{
    public event Action PlayerOnFinish;

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Hero>() != null)
            PlayerOnFinish?.Invoke();
        
    }
}
