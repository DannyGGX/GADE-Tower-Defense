using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearWaypointManager : WaypointManager
{
    [SerializeField] private Waypoint[] waypointsArray;

    private CustomLinkedList<Waypoint> waypointLinkedList;

    private void Awake()
    {
        BaseAwake();
        waypointLinkedList = PopulateLinkedList(waypointsArray);
    }

    protected override void HideWaypointMeshes()
    {
        foreach (var waypoint in waypointsArray)
        {
            waypoint.HideMesh();
        }
    }

    public override Node<Waypoint> GetWaypointLinkedListHead()
    {
        return waypointLinkedList.Head;
    }

    public override Node<Waypoint> GetPositionTrackerWaypointHead()
    {
        return waypointLinkedList.Head;
    }
}