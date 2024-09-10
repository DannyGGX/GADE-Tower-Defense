using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using Random = UnityEngine.Random;

namespace DannyG.Paths
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private Waypoint waypointPrefab;
        [SerializeField] private GameObject splineArrow;
        [Space]
        [SerializeField, Tooltip("The minimum space between waypoints and the main tower after the first waypoint is placed")] 
        private float personalSpaceRadius = 3;
        [SerializeField, Tooltip("Other waypoints will be in this layer") ] 
        private LayerMask layerToCheckForPersonalSpace;
        [Space]
        [Header("Distance range between waypoints")]
        [SerializeField] private float minDistance = 5;
        [SerializeField] private float maxDistance = 15;
        [SerializeField] private float maxAngleDeviationToTargetEndPoint = 5;
        [Space]
        [Header("Waypoints before pointing to target end point")] 
        [SerializeField] private int minWaypointsBeforeTargetingEndPoint = 2;
        [SerializeField] private int maxWaypointsBeforeTargetingEndPoint = 5;
        [SerializeField] private int waypointCountBeforeStartingTargetingEndPoint = 2;
        [Space] 
        [SerializeField] private int maxNumberOfWaypoints = 25;

        private int currentWaypointCount = 1; // starts at 1 because the first waypoint is already created at the main tower
        private int countForNextEndPointTargetting;

        private Stack<Vector3> waypointPositions; // Because waypoints are created backwards, the positions are saved before creating the actual linked list of waypoints.
        private Vector3 _targetEndPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public LinearWaypointPath CreatePath(Vector3 startPoint, Border border)
        {
            List<Waypoint> waypoints = new List<Waypoint>();
            waypoints.Add(CreateWaypoint(startPoint)); // place the start point first
            currentWaypointCount = 1;
            countForNextEndPointTargetting = waypointCountBeforeStartingTargetingEndPoint;
            _targetEndPoint = border.GetRandomPositionOnBorder();
            Debug.DrawLine(_targetEndPoint, _targetEndPoint.With(y: 10), Color.red, 120);
            
            Vector3 lastPosition = startPoint;

            while (currentWaypointCount < maxNumberOfWaypoints)
            {
                Vector3 currentPosition = CalculateNextPosition(lastPosition, GenerateAngle(lastPosition), GenerateDistance());
                if (currentPosition.ToGroundPosition(out Vector3 groundPosition) == false) break;
                currentPosition = groundPosition;
                if (CheckIfPositionIsValid(currentPosition))
                {
                    waypoints.Add(CreateWaypoint(currentPosition));
                    lastPosition = currentPosition;
                    currentWaypointCount++;

                    if (border.IsOutsideBorder(currentPosition)) break;
                }
            }
            waypoints.Reverse();
            return CreatePath(waypoints);
        }
        private bool CheckIfPositionIsValid(Vector3 position)
        {
            return !Physics.CheckSphere(position, personalSpaceRadius, layerToCheckForPersonalSpace);
        }

        private float GenerateAngle(Vector3 previousPosition)
        {
            if (currentWaypointCount != countForNextEndPointTargetting)
            {
                return Random.Range(0, 360);
            }
            else
            {
                countForNextEndPointTargetting += Random.Range(minWaypointsBeforeTargetingEndPoint, maxWaypointsBeforeTargetingEndPoint);
                
                float angle = Vector3.Angle(previousPosition - _targetEndPoint, Vector3.up);
                float minDeviationAngle = angle - maxAngleDeviationToTargetEndPoint;
                float maxDeviationAngle = angle + maxAngleDeviationToTargetEndPoint;
                return Random.Range(minDeviationAngle, maxDeviationAngle);
            }
        }

        private float GenerateDistance() => Random.Range(minDistance, maxDistance);

        private Vector3 CalculateNextPosition(Vector3 previousPosition, float angle, float distance)
        {
            float x = previousPosition.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = previousPosition.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector3(x, 0, z);
        }

        private Waypoint CreateWaypoint(Vector3 position)
        {
            Waypoint waypoint = Instantiate(waypointPrefab, position, Quaternion.identity, transform);
            //waypoint.gameObject.layer = layerToCheckForPersonalSpace;
            return waypoint;
        }

        private LinearWaypointPath CreatePath(List<Waypoint> waypoints)
        {
            var go = new GameObject
            {
                name = "Path"
            };
            go.transform.SetParent(transform);
            waypoints.ForEach(waypoint => waypoint.transform.SetParent(go.transform));
            var path = go.AddComponent<LinearWaypointPath>();
            var splineCreator = go.AddComponent<SplinePath>();
            
            var waypointsArray = waypoints.ToArray();
            splineCreator.GenerateSpline(waypointsArray, splineArrow);
            path.SetWaypointsArray(waypointsArray);
            return path;
        }
        
    }
    
    public static class RaycastHelper
    {
        public static float RaycastHeight = 50;
        public static float RaycastDistance = RaycastHeight + 10;
        
        
        public static bool ToGroundPosition(this Vector3 position, out Vector3 groundPosition)
        {
            position.y = RaycastHeight;
            if (Physics.Raycast(position, Vector3.down, out var hit, RaycastDistance) == false)
            {
                groundPosition = position;
                return false;
            }
            groundPosition = hit.point;
            return true;
        }
    }
    
}

