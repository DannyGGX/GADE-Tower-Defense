using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Waypoint : MonoBehaviour
{
    [field: SerializeField] public bool ShowGizmos { get; set; } = true;
    [SerializeField] private Color gizmosColor = Color.yellow;
    [SerializeField, Tooltip("Make sure it is set to onTrigger")] private Collider collider;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowGizmos == false) return;

        if (collider == null)
        {
            collider = GetComponent<Collider>();
        }
        Gizmos.color = gizmosColor;
        
        if (collider is SphereCollider sphereCollider)
        {
            Gizmos.DrawWireSphere(transform.position, sphereCollider.radius);
        }
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            //AI_Racer aiRacer = other.GetComponent<AI_Racer>();
            //aiRacer.SetNextDestination();
            //EventManager.OnAIWaypointPassed.Invoke(aiRacer.RacerID);
            // send an event to PositionTracker or directly call PositionTracker and pass the racer id
        }
        else if (other.CompareTag("Player"))
        {
            // send an event to PositionTracker or directly call PositionTracker and pass the racer id
            //EventManager.OnPlayerWaypointPassed.Invoke();
        }
    }
    
}
