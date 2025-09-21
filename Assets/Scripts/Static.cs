using UnityEngine;

public class Static : MonoBehaviour
{
    public Transform PlayerTransform;
    public Camera Camera;
    public float range = 2f;
    public LayerMask switchLayer;
    public Texture beamTexture; // Assign your texture in the inspector

    private LineRenderer beamRenderer;

    void Start()
    {
        beamRenderer = gameObject.AddComponent<LineRenderer>();
        Material beamMaterial = new Material(Shader.Find("Sprites/Default"));
        beamMaterial.mainTexture = beamTexture; // Use your texture
        beamMaterial.mainTextureScale = new Vector2(20f, 1f);
        beamRenderer.material = beamMaterial;
        beamRenderer.startWidth = 0.1f;
        beamRenderer.endWidth = 0.1f;
        beamRenderer.startColor = Color.white;
        beamRenderer.endColor = Color.white;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ShootCharge();
        }
    }

    void ShootCharge()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range, switchLayer))
        {
            SwitchController switchController = hit.transform.GetComponent<SwitchController>();
            if (switchController != null)
            {
                switchController.ToggleSwitch();
            }

            beamRenderer.positionCount = 2;
            beamRenderer.SetPosition(0, Camera.transform.position + Camera.transform.forward * 0.1f);
            beamRenderer.SetPosition(1, hit.point);
            Invoke("DisableBeam", 0.5f);
        }
    }

    void DisableBeam()
    {
        beamRenderer.positionCount = 0;
    }
}