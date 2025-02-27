using System.Collections.Generic;
using UnityEngine;


public class EnvironmentSwapScript : MonoBehaviour
{
    [SerializeField] private GameObject hallway;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ActivateEnv(GameObject environment, bool inEnv)
    {
        foreach (GameObject child in transform.GetComponentsInChildren<GameObject>())
        {
            if (child != environment)
            {
                child.SetActive(!inEnv);
            }
        }
    }
}
