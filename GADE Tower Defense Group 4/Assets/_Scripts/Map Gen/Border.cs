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
        
        public Vector3 GetRandomPositionOnBorder()
        {
            // Choose random side of the border
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0: // Top
                    return new Vector3(Random.Range(minX, maxX), 0, maxZ);
                case 1: // Bottom
                    return new Vector3(Random.Range(minX, maxX), 0, minZ);
                case 2: // Left
                    return new Vector3(minX, 0, Random.Range(minZ, maxZ));
                case 3: // Right
                    return new Vector3(maxX, 0, Random.Range(minZ, maxZ));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public bool IsOutsideBorder(Vector3 position)
        {
            return position.x < minX || position.x > maxX || position.z < minZ || position.z > maxZ;
        }
    }
}