using System;
using System.Linq;
using UnityEngine;

public class SetEnvActive : MonoBehaviour
{
    private GameObject player;
    public GameObject environment;
    public GameObject allEnvironments;

    [Header("Set the layer number here")]
    public string[] layerName;

    private Transform[] childTransforms;
    private Transform[] envTransforms;



    void Start()
    {
        // Find player by name
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found! Ensure the player GameObject is named 'Player'.");
        }

        if (environment == null)
        {
            Debug.LogError("Environment object is not assigned in the Inspector!");
            return;
        }

        // Cache child transforms for performance optimization
        childTransforms = environment.GetComponentsInChildren<Transform>();
        // Cache all environments EXCLUDING `environment` and its children
        envTransforms = allEnvironments
            .GetComponentsInChildren<Transform>()
            .Where(t => t != environment.transform && !childTransforms.Contains(t))
            .ToArray();
    }
    private void OnCollisionEnter(Collision collision)
    {
        SetLayer(0, collision);
        DeactivateEnvironments(false);
    }

    private void OnCollisionExit(Collision collision)
    {
        //SetLayer(layer, collision);
        DeactivateEnvironments(true);
    }

    private void SetLayer(int layerToSet, Collision coll)
    {
        if (coll.gameObject == player && environment != null)
        {
            environment.layer = layerToSet;

            // Update the cached child objects instead of searching for them every time
            foreach (Transform child in childTransforms)
            {
                child.gameObject.layer = layerToSet;
            }
        }
    }

    private void DeactivateEnvironments(bool state)
    {
        foreach (Transform child in envTransforms)
        {
            if (child != environment.transform && child != allEnvironments.transform)
            {
                child.gameObject.SetActive(state);
            }
        }
    }
}
