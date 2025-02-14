using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnvironmentLayer
{
    public string environmentName; // The name of the environment (e.g., "Beach", "House")
    public int layer;              // The layer associated with the environment
}

public class EnvironmentScript : MonoBehaviour
{
    [SerializeField] private GameObject hallway;
    [SerializeField] private List<EnvironmentLayer> environmentLayerList; // List of environment-layer pairs
    [SerializeField] private List<GameObject> exceptions; // List of exceptions
    [SerializeField] private List<GameObject> staticLayers; // List of exceptions

    private Dictionary<string, int> environmentLayerMap;
    private GameObject[] allEnvironments; // Array of environments

    private void Start()
    {
        // Get all children of this GameObject as environments
        List<GameObject> envList = new();
        foreach (Transform child in transform)
        {
            envList.Add(child.gameObject);
        }
        allEnvironments = envList.ToArray();

        // Initialize the dictionary
        environmentLayerMap = new Dictionary<string, int>();

        // Populate the dictionary with data from the Inspector
        foreach (var envLayer in environmentLayerList)
        {
            environmentLayerMap[envLayer.environmentName] = envLayer.layer;
        }

        // Disable colliders of all the worlds
        foreach (GameObject child in allEnvironments)
        {
            foreach (Collider child2 in child.GetComponentsInChildren<Collider>())
            {
                if (!child2.isTrigger) // leave colliders that are a trigger
                {
                    child2.enabled = false;
                }
            }
        }

        // Set the layers of the static objects
        foreach (GameObject child in staticLayers)
        {
            foreach (Transform child2 in child.GetComponentsInChildren<Transform>())
            {
                if (child2.CompareTag("Ezel")) {
                child2.gameObject.layer = environmentLayerMap[child.tag];
            } else if (child2.CompareTag("NoTouch")) {
                child2.gameObject.layer = 0;
            }
            else {
                child2.gameObject.layer = environmentLayerMap[child.name];
            }
            }
            
        }

    }

    public void ActivateEnv(GameObject environment, bool inEnv)
    {
        // Change layer of hallway
        foreach (Transform child in hallway.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = inEnv ? environmentLayerMap[hallway.name] : 0;
        }

        // Loop through the environments
        foreach (GameObject child in allEnvironments)
        {
            // Change layer of environments and enable (non-trigger) colliders
            if (child == environment)
            {
                // Change the layers of the current environment -> 0 means visible
                foreach (Transform transform in child.GetComponentsInChildren<Transform>())
                {
                    // change the layer of the object unless the tag is either "Castle" or "Ezel"
                    if (transform.gameObject.CompareTag("Castle") || transform.gameObject.CompareTag("Ezel")) {
                        continue;
                } else {
                        transform.gameObject.layer = inEnv ? 0 : environmentLayerMap[child.name];
                    }
                }

                // Toggle the Colliders of the current environment -> enable if inEnv is true
                foreach (Collider collider in child.GetComponentsInChildren<Collider>())
                {
                    if (!collider.isTrigger)
                    {
                        collider.enabled = inEnv;
                    }
                }
            }
            else // Toggle the state of all other environments -> Set to hallway layer if inEnv is true
            {
                foreach (Transform transform in child.GetComponentsInChildren<Transform>())
                {
                    transform.gameObject.layer = inEnv ? environmentLayerMap[hallway.name] : environmentLayerMap[child.name];
                }
            }
        }

        // Handle exceptions and its children
        foreach (GameObject exception in exceptions)
        {
            
            exception.layer = inEnv ? exception.layer : LayerMask.NameToLayer("TEMP");
            foreach (Transform child in exception.GetComponentsInChildren<Transform>())
            {
            child.gameObject.layer = inEnv ? child.gameObject.layer : LayerMask.NameToLayer("TEMP");
            }
        }
    }
}
