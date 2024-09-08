using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : Singleton<TerrainGenerator>
{
    Mesh mesh;
    public bool externallyControlled = false;
    [SerializeField] private int meshScale = 1;
    public AnimationCurve heightCurve;
    private Vector3[] vertices;
    private int[] triangles;

    public int xSize;
    public int zSize;

    public float scale; 
    public int octaves;
    public float lacunarity;

    public int seed;
    private System.Random _prng;
    private Vector2[] _octaveOffsets;

    protected override void Awake()
    {
        base.Awake();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // private void Start()
    // {
    //     if (externallyControlled) return; // For when another script needs to control when the map needs to be generated
    //     CreateNewMap();
    // }

    public void CreateNewMap()
    {
        CreateMeshShape();
        CreateTriangles();
        UpdateMesh();
    }

    /// <summary>
    /// The Map manager will call this function
    /// </summary>
    /// <param name="xSize"></param>
    /// <param name="zSize"></param>
    public void SetValues(int xSize, int zSize)
    {
        externallyControlled = true;
        this.xSize = xSize;
        this.zSize = zSize;
    }

    public void SetValues(int xSize, int zSize, float scale, int octaves, float lacunarity)
    {
        externallyControlled = true;
        this.xSize = xSize;
        this.zSize = zSize;
        this.scale = scale;
        this.octaves = octaves;
        this.lacunarity = lacunarity;
    }

    private void CreateMeshShape ()
    {
        // Creates seed
        Vector2[] octaveOffsets = GetOffsetSeed();

        if (scale <= 0)
            scale = 0.0001f;
            
        // Create vertices array
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                // Assign and set height of each vertices
                float noiseHeight = GenerateNoiseHeight(z, x, octaveOffsets);
                vertices[i] = new Vector3(x, noiseHeight, z);
                i++;
            }
        }
    }

    private Vector2[] GetOffsetSeed()
    {
        seed = Random.Range(0, 1000);
        // changes area of map
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
                    
        for (int o = 0; o < octaves; o++) {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[o] = new Vector2(offsetX, offsetY);
        }
        return octaveOffsets;
    }

    private float GenerateNoiseHeight(int z, int x, Vector2[] octaveOffsets)
    {
        float amplitude = 12;
        float frequency = 1;
        float persistence = 0.5f;
        float noiseHeight = 0;

        for (int y = 0; y < octaves; y++)
        {
            float mapZ = z / scale * frequency + octaveOffsets[y].y;
            float mapX = x / scale * frequency + octaveOffsets[y].x;

            // Create perlinValues  
            // The *2-1 is to create a flat floor level
            float perlinValue = Mathf.PerlinNoise(mapZ, mapX) * 2 - 1;
            noiseHeight += heightCurve.Evaluate(perlinValue) * amplitude;
            frequency *= lacunarity;
            amplitude *= persistence;
        }
        return noiseHeight;
    }

    private void CreateTriangles() 
    {
        // Need 6 vertices to create a square (2 triangles)
        triangles = new int[xSize * zSize * 6];
        int vertexIndex = 0;
        int triangleIndex = 0;

        // loop through rows
        for (int z = 0; z < xSize; z++)
        {
            // fill all columns in row
            for (int x = 0; x < xSize; x++)
            {
                triangles[triangleIndex + 0] = vertexIndex + 0;
                triangles[triangleIndex + 1] = vertexIndex + xSize + 1;
                triangles[triangleIndex + 2] = vertexIndex + 1;
                triangles[triangleIndex + 3] = vertexIndex + 1;
                triangles[triangleIndex + 4] = vertexIndex + xSize + 1;
                triangles[triangleIndex + 5] = vertexIndex + xSize + 2;

                vertexIndex++;
                triangleIndex += 6;
            }
            vertexIndex++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        GetComponent<MeshCollider>().sharedMesh = mesh;

        gameObject.transform.localScale = new Vector3(meshScale, meshScale, meshScale);
    }

    public Vector3 GetMapCenter()
    {
        float x = (xSize - 1) / 2f;
        float z = (zSize - 1) / 2f;
        return new Vector3(x, 0, z);
    }
    
    public float GetHighestElevation()
    {
        float highestPoint = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y > highestPoint)
            {
                highestPoint = vertices[i].y;
            }
        }
        return highestPoint;
    }
    
    
}
