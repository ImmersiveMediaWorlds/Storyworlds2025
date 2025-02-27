using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // When 'R' is pressed
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
        }
    }
}
