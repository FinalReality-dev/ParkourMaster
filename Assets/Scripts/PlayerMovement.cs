using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";
    public string jump = "Jump";

    [Header("Movement")]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 20.0f;
    public float gravity = -20.0f;

    [Header("Player Jump")]
    public float radius = 10.0f;
    public int score = 0;

    bool hitPlayer = false;

    CharacterController controller;
    Vector3 moveVelocity = new Vector3();
    Vector3 turnVelocity = new Vector3();

    void Awake(){
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var hInput = Input.GetAxis(horizontalInput);
        var vInput = Input.GetAxis(verticalInput);

        turnVelocity = transform.up * rotationSpeed * hInput;
        if(controller.isGrounded){
            hitPlayer = false;
            moveVelocity = transform.forward * vInput * moveSpeed;
            if(Input.GetButton(jump)){
                moveVelocity.y = jumpSpeed;
            }
        } else {
            moveVelocity.y += gravity * Time.deltaTime;
            if(!hitPlayer){
                RaycastHit hit;
                if(Physics.SphereCast(transform.position, radius, -1 * transform.up, out hit)){
                    if(hit.collider.gameObject.GetComponent<PlayerMovement>()){
                        Debug.Log("Jumped Player: " + gameObject);
                        hitPlayer = true;
                        score += 1;
                    }
                }
            }
        }

        controller.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }
}
