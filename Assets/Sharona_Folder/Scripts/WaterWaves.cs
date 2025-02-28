using UnityEngine;

public class WaterWaves : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1.0f;
    public float waveFrequency = 1.0f;

    private MeshFilter meshFilter;
    private Vector3[] baseVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        baseVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + vertex.x * waveFrequency) * waveHeight;
            vertices[i] = vertex;
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();
    }
}
