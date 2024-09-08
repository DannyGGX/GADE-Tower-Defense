using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
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

    public void HideMesh()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
