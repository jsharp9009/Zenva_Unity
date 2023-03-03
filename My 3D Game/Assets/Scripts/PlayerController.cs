using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public int score;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float z = Input.GetAxisRaw("Vertical") * moveSpeed;

        player.velocity = new Vector3(x, player.velocity.y, z);

        Vector3 velocity = player.velocity;
        velocity.y = 0;
        if(velocity.x != 0 || velocity.z != 0 ){
            transform.forward = velocity;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if(transform.position.y < -5)
            GameOver();
    }

    void OnCollisionEnter(Collision collision){
        if(collision.GetContact(0).normal == Vector3.up) isGrounded = true;
    }

    public void GameOver(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount){
        score += amount;
        text.text = "Score: " + score;
    }
}
