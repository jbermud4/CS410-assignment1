using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    //jump code
    private float jumpForce = 6f;
    private int maxJumps = 2;
    private int jumpsRemaining;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        jumpsRemaining = maxJumps;
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 14){
            winTextObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Ground")){
            jumpsRemaining = maxJumps;
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            if (jumpsRemaining > 0){
                Jump();
                jumpsRemaining--;
            }
        }
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // picking up collectibles
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}
