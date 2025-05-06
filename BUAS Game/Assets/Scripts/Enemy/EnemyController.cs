using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float chaseRange = 5f;
    public float attackRange = 1.2f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool playerInRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (target == null || !playerInRange || !agent.isOnNavMesh)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > chaseRange)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            agent.isStopped = true;
        }
        else if (distance > attackRange)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            agent.isStopped = true;
            transform.LookAt(new Vector3(target.position.x, transform.position.y, transform.position.z)); // Gira hacia el jugador
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            agent.isStopped = true;
        }
    }
}
