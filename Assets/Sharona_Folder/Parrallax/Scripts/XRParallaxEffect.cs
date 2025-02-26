using UnityEngine;

public class XRParallaxEffect : MonoBehaviour
{
    public Transform xrCamera;  // De XR-hoofdpositie (HMD)
    public float parallaxFactor = 0.5f;
    private Vector3 previousCameraPosition;

    // Limieten voor de Z-beweging
    public float minZ = -5f;
    public float maxZ = 5f;

    void Start()
    {
        // Automatisch de XR-hoofdpositie detecteren als niet ingesteld
        if (xrCamera == null)
        {
            xrCamera = Camera.main?.transform;  // Valt terug op Camera.main als er geen XR-camera is
        }

        if (xrCamera == null)
        {
            Debug.LogError("❌ XR Camera is NOT set! Zorg dat je de juiste XR-camera toevoegt.");
        }
        else
        {
            Debug.Log("✅ XR Camera gevonden: " + xrCamera.name);
            previousCameraPosition = xrCamera.position;
        }
    }

    void Update()
    {
        if (xrCamera == null) return;

        Vector3 deltaMovement = xrCamera.position - previousCameraPosition;

        // 🔍 Debug om te checken of de beweging klopt
        Debug.Log($"📌 Delta Movement: {deltaMovement}");

        // 👉 Als het object verkeerd beweegt, verander dit naar `-deltaMovement.z`
        float newZ = transform.position.z - deltaMovement.z * parallaxFactor;

        // Beperk de Z-waarde tussen minZ en maxZ
        newZ = Mathf.Clamp(newZ, minZ, maxZ);

        // Update alleen de Z-positie, X en Y blijven hetzelfde
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // Update de vorige positie
        previousCameraPosition = xrCamera.position;
    }
}

