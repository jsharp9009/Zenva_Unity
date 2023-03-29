using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D body;
    public SpriteRenderer sr;
    public LayerMask interactLayerMask;
    private Vector2 moveInput;
    private bool interactionInput;
    private Vector2 facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveInput.magnitude != 0.0f ){
            facingDirection = moveInput.normalized;
            if(moveInput.x != 0)
                sr.flipX = moveInput.x > 0;
        }

        if(interactionInput){
            TryInteractTile();
            interactionInput = false;
        }
    }

    void FixedUpdate(){
        body.velocity = moveInput.normalized * moveSpeed;
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnInteractInput(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            interactionInput = true;
        }
    }

    void TryInteractTile(){
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + facingDirection, Vector3.up, 0.1f, interactLayerMask);

        if(hit.collider != null){
            FieldTile tile = hit.collider.GetComponent<FieldTile>();
            tile.Interact();
        }
    }
}
