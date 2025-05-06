using UnityEngine;
using UnityEngine.AI;

public class EnemyDistanceController : MonoBehaviour
{
    public Transform target;
    public float activationDistance = 5f;  // Distancia de detección
    public float stopDistance = 2.5f;      // Distancia mínima para atacar

    private NavMeshAgent navEnemy;
    private Animator animator;

    void Awake()
    {
        navEnemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navEnemy.updateRotation = false;
        navEnemy.updateUpAxis = false;
    }

    void Update()
    {
        if (target == null || !navEnemy.isOnNavMesh)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= activationDistance)
        {
            if (distance > stopDistance)
            {
                // Perseguir al jugador
                Vector3 targetPosition = target.position;
                targetPosition.x = transform.position.x; // mantener X si solo se mueve en Z

                navEnemy.destination = targetPosition;
                navEnemy.isStopped = false;

                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);

                // Rotación manual
                Vector3 lookDir = target.position - transform.position;
                lookDir.x = 0;
                lookDir.y = 0;
                if (lookDir != Vector3.zero)
                    transform.forward = lookDir.normalized;
            }
            else
            {
                // Está en rango de ataque
                navEnemy.isStopped = true;

                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
            }
        }
        else
        {
            // Muy lejos → volver a Idle
            navEnemy.isStopped = true;

            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
        }
    }
}
