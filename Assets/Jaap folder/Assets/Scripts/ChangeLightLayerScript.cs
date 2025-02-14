using UnityEngine;

public class ChangeLightLayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeLayerRecursively(transform, 1);
    }

    void ChangeLayerRecursively(Transform parent, int layer)
        {
            foreach (Transform child in parent)
            {
            if (child.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                meshRenderer.renderingLayerMask = 2; // 2 = "Mountain"
            }
            ChangeLayerRecursively(child, layer);
            }
        }
}
