using System;
using DannyG._Scripts.Units.Buildings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DannyG.Buildings
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Border))]
    public class MainTowerCreator : MonoBehaviour
    {
        [SerializeField] private float minElevation = 1;
        [SerializeField] private float maxElevation = 5;
        [SerializeField] private float minSteepness = 0;
        [SerializeField] private float maxSteepness = 45;
        [Space]
        [SerializeField] private MainTower towerPrefab;

        private const float RaycastHeight = 10;
        private const float RaycastDistance = 100;
        private Border _border;
        
        private void Awake()
        {
            _border = GetComponent<Border>();
        }

        public void PlaceTower()
        {
            RaycastHit hit;
            while (true)
            {
                Vector3 position = _border.GetRandomPosition();
                position.y = RaycastHeight;

                Physics.Raycast(position, Vector3.down, out hit, RaycastDistance);

                if (hit.point.y < minElevation || hit.point.y > maxElevation)
                {
                    continue;
                }

                if (hit.normal.y < minSteepness || hit.normal.y > maxSteepness)
                {
                    continue;
                }
                break;
            }

            MainTower mainTower = Instantiate(towerPrefab, hit.point, Quaternion.identity);
            if (BuildingManager.Instance == null)
            {
                Debug.Log("No building manager in scene");
                return;
            }
            BuildingManager.Instance.MainTower = mainTower;
        }
    }
}