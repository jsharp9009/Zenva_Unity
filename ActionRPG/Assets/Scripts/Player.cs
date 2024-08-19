using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody rig;
    public int currHP;
    public int maxHP;
    public float jumpForce;
    public float attackDistance;
    public int damage;
    public bool isAttacking;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking){
            Attack();
        }

        if(!isAttacking)
            UpdateAnimator();
    }

    void Move(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
    }

    void Jump(){
        if(CanJump()){
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool CanJump(){
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.1f)){
            return hit.collider != null;
        }
        return false;
    }

    public void TakeDamage(int damage){
        currHP -= damage;
        HealthBarUI.instance.UpdateFill(currHP, maxHP);
        if(currHP <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Attack(){
        isAttacking = true;
        animator.SetTrigger(ATTACK);

        Invoke("TryDamage", .7f);
        Invoke("DisableIsAttacking", 2.66f);    }

    void TryDamage(){
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray,1f, attackDistance, 1 << 8);
        foreach(RaycastHit hit in hits){
            hit.collider.GetComponent<Enemy>()?.TakeDamage(damage);
        }
    }

    void DisableIsAttacking(){
        isAttacking = false;
    }

    private const string FORWARD = "movingForward", BACKWARD = "movingBackward", LEFT = "movingLeft", RIGHT = "movingRight", ATTACK = "Attack";


    void UpdateAnimator(){
        animator.SetBool(FORWARD, false);
        animator.SetBool(BACKWARD, false);
        animator.SetBool(LEFT, false);
        animator.SetBool(RIGHT, false);

        Vector3 localVelocity = transform.InverseTransformDirection(rig.velocity);
        
        if(localVelocity.z > 0.1f)
            animator.SetBool(FORWARD, true);
        else if(localVelocity.z < -.1f)
            animator.SetBool(BACKWARD, true);
        else if(localVelocity.x > 0.1f)
            animator.SetBool(RIGHT, true);
        else if(localVelocity.x < -.1f)
            animator.SetBool(LEFT, true);
        
    }
}
