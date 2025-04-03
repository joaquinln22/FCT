using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public bool enemyMove;
    private NavMeshAgent navEnemy;

    void Awake()
    {
        navEnemy = GetComponent<NavMeshAgent>();
        navEnemy.updateRotation = false; // Evita rotaciones automáticas del agente
        navEnemy.updateUpAxis = false;   // Previene inclinaciones si las hubiera
    }

    void Update()
    {
        if (target != null && navEnemy.isOnNavMesh && enemyMove)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > 2.5f)
            {
                // Restringimos el movimiento al plano Y-Z
                Vector3 targetPosition = target.position;
                targetPosition.x = transform.position.x;
                navEnemy.destination = targetPosition;

                // Rotación manual en eje Z (como el jugador)
                float directionZ = target.position.z - transform.position.z;
                if (directionZ != 0f)
                {
                    Vector3 lookDirection = new Vector3(0f, 0f, Mathf.Sign(directionZ));
                    transform.forward = lookDirection;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && navEnemy.isOnNavMesh){
            float distance = Vector3.Distance(transform.position, target.position);
            
            if(distance >= 2.5f){
                Vector3 targetPosition = target.position;

                // Bloqueamos la posición X para que siempre se mantenga
                targetPosition.x = transform.position.x;

                navEnemy.destination = targetPosition;
                navEnemy.isStopped = false;
                enemyMove = true;  
            }else{
                navEnemy.isStopped = true;
                enemyMove = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && navEnemy.isOnNavMesh){
            navEnemy.isStopped = true;
            enemyMove = false;
        }
    }
}