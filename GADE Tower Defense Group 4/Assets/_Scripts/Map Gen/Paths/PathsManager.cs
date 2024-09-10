using System.Collections.Generic;
using DannyG._Scripts.Units.Buildings;
using UnityEngine;
using UnityUtils;

namespace DannyG.Paths
{
    public class PathsManager : Singleton<PathsManager>
    {
        [SerializeField] private int numberOfPaths = 3;
        [SerializeField] private PathCreator pathCreator;

        private LinearWaypointPath[] paths;

        public void CreatePaths()
        {
            paths = new LinearWaypointPath[numberOfPaths];
            for (int i = 0; i < numberOfPaths; i++)
            {
                paths[i] = pathCreator.CreatePath(BuildingManager.Instance.MainTower.transform.position,
                    MapManager.Instance.mapBorder);
            }
        }
        
        
        
    }
}