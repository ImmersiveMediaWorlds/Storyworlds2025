using UnityEngine;

public class MultiLayerParallax : MonoBehaviour
{
    public Transform player; // De XR Camera of XR Origin
    public Transform[] layers; // Array van 4 lagen voor het parallax effect
    public float[] parallaxFactors = { 0.01f, 0.02f, 0.03f, 0.04f }; // Hoe sterk elke laag beweegt
    public float maxZOffset = 0.5f; // Maximale z-positie offset voordat de beweging stopt

    private Vector3[] initialPositions; // Beginposities van de lagen

    void Start()
    {
        if (layers.Length != 4)
        {
            Debug.LogError("Zorg ervoor dat er exact 4 lagen zijn toegewezen in de inspector!");
            return;
        }

        initialPositions = new Vector3[layers.Length];

        // Sla de beginposities van de lagen op
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
        }
    }

    void Update()
    {
        if (player == null) return;

        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] == null) continue;

            // Bereken de offset gebaseerd op de spelerpositie en de parallax factor
            Vector3 offset = (player.position - transform.position) * parallaxFactors[i];

            // Pas de offset toe, maar behoud de originele Y en Z positie binnen de limiet
            float newZ = Mathf.Clamp(initialPositions[i].z + offset.z, initialPositions[i].z - maxZOffset, initialPositions[i].z + maxZOffset);

            layers[i].position = new Vector3(initialPositions[i].x + offset.x, initialPositions[i].y + offset.y, newZ);
        }
    }
}

