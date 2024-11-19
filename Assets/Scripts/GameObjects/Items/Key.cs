using UnityEngine;

public class Key : Item
{
    [SerializeField] private string gateTag = ""; // Tag del port�n que puede desbloquear esta llave

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con el port�n correcto
        if (collision.collider.CompareTag(gateTag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
