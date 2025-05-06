using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float playerRotate;
    public float jumpSpeed;
    public bool playerMove = false;
    public bool checkGround = true;
    public Transform chkGround;
    public Transform atkPoint;
    public float atkRange;
    public LayerMask enemyLayers;

    private Rigidbody rb;
    private Vector3 displacement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float mh = Input.GetAxis("Horizontal");
        PlayerMove(mh);
        PlayerJumper();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerMove(float mh)
    {
        displacement.Set(0f, 0f, mh);
        displacement = displacement.normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + displacement);

        if(mh != 0f){
            PlayerRotate(mh);
        }

        bool playerRun = mh != 0f;

        if(playerRun){
            playerMove = true;
        }else{
            playerMove = false;
        }
    }

    void PlayerRotate(float mh)
    {
        float interpolation = playerRotate * Time.deltaTime;
        Vector3 targetDirection = new Vector3(0f, 0f, mh);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, interpolation);
        rb.MoveRotation(newRotation);
    }

    void PlayerJumper(){
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        if(Input.GetButton("Jump") && checkGround){
            rb.linearVelocity = new Vector3(0f, jumpSpeed, 0f);
            checkGround = false;
        }

        if(Physics.Raycast(chkGround.position, dwn, out hit, 0.2f) && hit.collider.CompareTag("Ground")){
            checkGround = true;
        }else{
            checkGround = false;
        }
    }
    
    public void PlayerAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(atkPoint.position, atkRange, enemyLayers);

        foreach (Collider hitenemy in hitColliders)
        {
            EnemyHealth enemy = hitenemy.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(10); // o cualquier valor de daño que quieras
                Debug.Log("⚔️ Dañando a: " + hitenemy.name);
            }
        }
    }

}
