using UnityEngine;

public class Key : Item
{
    [SerializeField] private string gateTag = ""; // Tag del portón que puede desbloquear esta llave

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con el portón correcto
        if (collision.collider.CompareTag(gateTag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
