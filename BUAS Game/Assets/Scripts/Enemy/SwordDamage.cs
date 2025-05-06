using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🗡️ Atacando al jugador");
            // Aquí puedes aplicar daño real
        }
    }
}