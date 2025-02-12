using UnityEngine;

public class ChangeSkyboxScript : MonoBehaviour
{
    // Update is called once per frame
    public void SetSkyboxMat(Material SkyBoxMat)
    {
        RenderSettings.skybox = SkyBoxMat;
    }
}
