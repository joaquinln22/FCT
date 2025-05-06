using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 10;
    private bool canDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    // Puedes activar esto con un evento en la animación si quieres más precisión
    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;
}