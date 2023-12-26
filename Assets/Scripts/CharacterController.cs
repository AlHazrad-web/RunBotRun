using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour {

    public static CharacterController instance;

    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody rigidbody;
    Rigidbody[] rigidbodies;
    private Animator animator;
    public bool isGrounded;
    public bool isWall;
    public Quaternion rotation = Quaternion.identity;

    public bool death = false;

    public Transform cameraTransform;

    private void Awake() {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update() {

        gameObject.transform.position = Vector3.Lerp(cameraTransform.transform.position, transform.position, 10 * Time.deltaTime);

        if (!isGrounded && !isWall && Physics.CheckSphere(groundCheck.position, 1.5f, groundLayer)) {
            if (rigidbody.velocity.z == 0.0f && rigidbody.velocity.y < 0.0f) {
                animator.SetBool("Falling", false);
                animator.SetBool("JumpDown", true);
            }
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded) {
            animator.SetBool("Falling", false);
            animator.SetBool("JumpDown", false);
        }
        if (!isGrounded && rigidbody.velocity.y != 0.0f) {
            animator.SetBool("Falling", true);
        }

        if (Physics.CheckBox(new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), new Vector3(0.29f, 0.8f, 0.29f), Quaternion.identity, wallLayer)) {
            isWall = true;
        } else {
            isWall = false;           
        }       
       
        float moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveInput = moveInput * 2.0f;
        }       
        rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, moveInput * moveSpeed);
        if (moveInput != 0.0f) {
            rotation = Quaternion.LookRotation(new Vector3(0f, 0f, moveInput));          
            animator.SetFloat("Speed", CalculateScpeed());
        } else {
            animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), 0.0f, Time.deltaTime * 10.0f));
        }
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, Time.deltaTime * 10.0f);
        if (!isGrounded && isWall) {
            if (moveInput != 0.0f) {
                rigidbody.velocity = Vector3.down * Time.deltaTime * 200.0f;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump")) {
            animator.SetBool("JumpUp", true);           
        }
       
       
    }

    //private void FixedUpdate() {
    //    if (!isGrounded && isWall) {
    //        float moveInput = Input.GetAxis("Horizontal");
    //        if (moveInput != 0f) {
    //            rigidbody.velocity = Vector3.down * Time.deltaTime * 200.0f;
    //        }
    //    }
    //}

    public void Jump() {
        Debug.Log("Jump");
        animator.SetBool("JumpUp", false);      
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    float CalculateScpeed() {
        float setspeed = Mathf.Lerp(animator.GetFloat("Speed"), 1.0f, Time.deltaTime * 10);
        if (Input.GetKey(KeyCode.LeftShift)) {
            setspeed += 1.0f;
        }
        if (setspeed > 2.0f) {
            setspeed = 2.0f;
        }
        return setspeed;
    }

    public void Death() {
        death = true;
        rigidbody.isKinematic = true;
        this.enabled = false;
        animator.enabled = false;
        StartCoroutine(EndDeath());
    }
    IEnumerator EndDeath() {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < rigidbodies.Length; i++) {
            rigidbodies[i].isKinematic = true;
        }
        UIGame.instance.EndGame();
    }

}
