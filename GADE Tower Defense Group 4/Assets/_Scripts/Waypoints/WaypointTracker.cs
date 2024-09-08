using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTracker
{
    private Node<Waypoint> currentNode;

    public Vector3 GetFirstWaypointPosition()
    {
        currentNode = WaypointManager.Instance.GetWaypointLinkedListHead();
        return GetCurrentWaypointPosition();
    }
    public Vector3 GetNextWaypointPosition()
    {
        SetNextNode();
        return GetCurrentWaypointPosition();
    }

    private void SetNextNode()
    {
        currentNode = currentNode.NextNode;
    }

    private Vector3 GetCurrentWaypointPosition()
    {
        return currentNode.Data.transform.position;
    }
}
