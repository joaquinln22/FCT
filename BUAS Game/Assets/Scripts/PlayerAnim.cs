using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private PlayerController player;
    private Animator playerAnim;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        playerAnim = GetComponent<Animator>();   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        RunAnim();
        JumpAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RunAnim(){
        if(player.playerMove){
            playerAnim.SetBool("Run", true);
        }else{
            playerAnim.SetBool("Run", false);
        }
    }

    void JumpAnim(){
        if(player.checkGround == false){
            playerAnim.SetBool("Jump", true);
        }else{
            playerAnim.SetBool("Jump", false);
        }
    }
}
