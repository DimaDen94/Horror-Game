using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTransition : Transition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if (Target != null && Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                Debug.Log(hit.transform.gameObject);
                if(hit.transform.gameObject.GetComponent<Hero>() != null)
                    NeedTransit = true;
            }
        }
    }
}