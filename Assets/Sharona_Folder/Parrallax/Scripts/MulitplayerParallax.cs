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

            // Bereken alleen de Z-offset gebaseerd op de spelerbeweging
            float zOffset = (player.position.z - transform.position.z) * parallaxFactors[i];

            // Beperk de beweging binnen maxZOffset grenzen
            float newZ = Mathf.Clamp(initialZPositions[i] + zOffset, initialZPositions[i] - maxZOffset, initialZPositions[i] + maxZOffset);

            // Pas alleen de Z-positie aan, X en Y blijven gelijk
            layers[i].position = new Vector3(layers[i].position.x, layers[i].position.y, newZ);
        }
    }
}


