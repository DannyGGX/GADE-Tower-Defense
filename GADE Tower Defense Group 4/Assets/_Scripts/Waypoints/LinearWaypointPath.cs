using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearWaypointPath : WaypointPath
{
    [SerializeField] private Waypoint[] waypointsArray;

    private CustomLinkedList<Waypoint> waypointLinkedList;

    // private void Awake()
    // {
    //     waypointLinkedList = PopulateLinkedList(waypointsArray);
    // }
    
    protected override void ToggleGizmosVisibility(bool show = false)
    {
        foreach (var waypoint in waypointsArray)
        {
            waypoint.ShowGizmos = show;
        }
    }

    public override Node<Waypoint> GetWaypointLinkedListHead()
    {
        return waypointLinkedList.Head;
    }

    public override Node<Waypoint> GetWaypointHead()
    {
        return waypointLinkedList.Head;
    }
    
    public void SetWaypointsArray(Waypoint[] waypointsArray)
    {
        
        this.waypointsArray = waypointsArray;
        
        waypointLinkedList = PopulateLinkedList(waypointsArray);
    }
}