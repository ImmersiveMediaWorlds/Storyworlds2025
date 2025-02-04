using UnityEngine;

public class ChangeSkyboxScript : MonoBehaviour
{
    [SerializeField] private Material SkyBoxMat;

    // Update is called once per frame
    public void SetSkyboxMat()
    {
        RenderSettings.skybox = SkyBoxMat;
    }
}
