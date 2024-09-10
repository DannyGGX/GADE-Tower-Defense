using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class WaypointPath : MonoBehaviour
{
    public bool showGizmos = false;
    
    protected abstract void ToggleGizmosVisibility(bool show = false);
    
    public abstract Node<Waypoint> GetWaypointLinkedListHead();
    
    /// <summary>
    /// Waypoints to be used by the position tracker.
    /// </summary>
    /// <returns> Head of the linked list of waypoints</returns>
    public abstract Node<Waypoint> GetWaypointHead();
    
    protected CustomLinkedList<Waypoint> PopulateLinkedList(Waypoint[] waypointsArray)
    {
         CustomLinkedList<Waypoint> waypointLinkedList = new CustomLinkedList<Waypoint>();
        foreach (var waypoint in waypointsArray)
        {
            waypointLinkedList.Add(waypoint);
        }
        return waypointLinkedList;
    }

    private void OnValidate()
    {
        ToggleGizmosVisibility(showGizmos);
    }
}
