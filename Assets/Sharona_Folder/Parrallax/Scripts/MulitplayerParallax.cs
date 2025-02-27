using UnityEngine;

public class MultiLayerParallax : MonoBehaviour
{
    public Transform player; // De XR Camera of XR Origin
    public Transform[] layers; // De lagen voor het parallax effect
    public float[] parallaxFactors = { 0.01f, 0.02f, 0.03f, 0.04f }; // Hoe sterk elke laag beweegt
    public float maxZOffset = 0.5f; // Maximale Z-afwijking

    private float[] initialZPositions; // Begin Z-posities van de lagen

    void Start()
    {
        if (layers.Length != parallaxFactors.Length)
        {
            Debug.LogError("Het aantal lagen en parallax factoren moet gelijk zijn!");
            return;
        }

        initialZPositions = new float[layers.Length];

        // Sla de originele Z-posities op
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] != null)
            {
                initialZPositions[i] = layers[i].position.z;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] == null) continue;

            // Reverse the Z-offset calculation
            float zOffset = (transform.position.z - player.position.z) * parallaxFactors[i];

            // Clamp movement to prevent excessive shifting
            float newZ = Mathf.Clamp(initialZPositions[i] + zOffset, initialZPositions[i] - maxZOffset, initialZPositions[i] + maxZOffset);

            // Apply new position while keeping original X and Y
            layers[i].localPosition = new Vector3(layers[i].localPosition.x, layers[i].localPosition.y, newZ);
        }

    }
}


