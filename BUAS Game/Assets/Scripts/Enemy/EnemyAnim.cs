using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator enemyAnim;
    private EnemyController enemy;

    void Awake()
    {
        enemy = FindObjectOfType<EnemyController>();
        enemyAnim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RunAnim();
    }

    void RunAnim(){
        if (enemy.enemyMove){
            enemyAnim.SetBool("Run", true);
        }else{
            enemyAnim.SetBool("Run", false);
        }
    }
}
