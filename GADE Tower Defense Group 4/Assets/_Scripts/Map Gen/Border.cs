using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DannyG
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private float xSize = 10;
        [SerializeField] private float zSize = 10;
        [SerializeField] private Vector3 centerPoint = new Vector3(50, 5, 50);
        [SerializeField] private Color gizmosColor = Color.yellow;
        
        public float maxX => centerPoint.x + xSize / 2f;
        public float minX => centerPoint.x - xSize / 2f;
        public float maxZ => centerPoint.z + zSize / 2f;
        public float minZ => centerPoint.z - zSize / 2f;
        

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireCube(centerPoint, new Vector3(xSize, 2, zSize));
        }
        
        public Vector3 GetRandomPosition()
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            return new Vector3(x, 0, z);
        }
    }
}