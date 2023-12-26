using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour {

    public static CharacterController instance;

    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;
    Rigidbody[] rigidbodies;
    Animator animator;
    public bool isGrounded;
    public bool isWall;
    public Quaternion rotation = Quaternion.identity;

    public bool death = false;

    public Transform cameraTransform;

    //public float up;   
    //public Vector3 testsize = new Vector3(0.29f, 0.8f, 0.29f);
    //GameObject go;

    private void Awake() {
        instance = this;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        groundCheck = gameObject.transform.Find("groundCheck").transform;
        //go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    void FixedUpdate() {
        cameraTransform.transform.position = Vector3.Lerp(cameraTransform.transform.position, transform.position, 10 * Time.deltaTime);
    }

   
    private void Update() {
       
       

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
        if (!animator.GetBool("Falling")) {
            capsuleCollider.center = new Vector3(0.0f, 0.91f, 0.0f);
            capsuleCollider.height = 1.82f;
        } else {
            capsuleCollider.center = new Vector3(0.0f, 0.57f, 0.0f);
            capsuleCollider.height = 1.28f;
        }

        Vector3 size = new Vector3(0.29f, 0.72f, 0.29f);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.71f, transform.position.z);
        //go.transform.position = pos;
        //go.transform.localScale = testsize;
        if (Physics.CheckBox(pos, size, Quaternion.identity, wallLayer)) {
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
