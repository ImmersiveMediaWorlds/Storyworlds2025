using UnityEngine;

public class TerrainToMesh : MonoBehaviour
{
    public Terrain terrain;
    public int resolution = 256; // Aantal vertices per rij/kolom

    void Start()
    {
        ConvertTerrainToMesh();
    }

    void ConvertTerrainToMesh()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = resolution;
        int height = resolution;
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        Vector3[] vertices = new Vector3[width * height];
        Vector2[] uv = new Vector2[width * height];
        int[] triangles = new int[(width - 1) * (height - 1) * 6];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                float y = heights[z, x] * terrainData.size.y;
                vertices[z * width + x] = new Vector3(x, y, z);
                uv[z * width + x] = new Vector2((float)x / width, (float)z / height);
            }
        }

        int triIndex = 0;
        for (int z = 0; z < height - 1; z++)
        {
            for (int x = 0; x < width - 1; x++)
            {
                int index = z * width + x;

                triangles[triIndex++] = index;
                triangles[triIndex++] = index + width;
                triangles[triIndex++] = index + width + 1;

                triangles[triIndex++] = index;
                triangles[triIndex++] = index + width + 1;
                triangles[triIndex++] = index + 1;
            }
        }

        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv
        };

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
    }
}

