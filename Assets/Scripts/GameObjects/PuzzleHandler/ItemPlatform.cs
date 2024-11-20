using UnityEngine;

public class ItemPlatform : MonoBehaviour
{
    public bool isColliding;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Color startColor;
    private MeshRenderer mesh;
    private Material material;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
        material.SetColor("_Color", startColor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == prefab)
        {
            material.SetColor("_Color", Color.green);
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        material.SetColor("_Color", startColor);
        isColliding = false;
    }
}
