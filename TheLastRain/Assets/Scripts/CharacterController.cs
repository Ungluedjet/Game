using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance
    {
        get
        {
            return s_Instance;
        }
    }

    private static CharacterController s_Instance;

    [SerializeField]private float moveSpeed = 5;
    [SerializeField]private float rotationSpeed = 5;
    [SerializeField]private float jumpSize = 3;
    [SerializeField]private float characterLife = 5;

    public bool detectable = true;


    private float hr;
    private float vr;
    private Rigidbody rb;
    private Animator anim;

    private Vector3 move;
    private Vector3 rotate;

    private bool characterOnGround = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        s_Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement() {
        hr = Input.GetAxis("Horizontal");
        vr = Input.GetAxis("Vertical");

        move = new Vector3(hr, 0f, vr);
        move.Normalize();


        if (Input.GetKey(KeyCode.A) == true || Input.GetKey(KeyCode.S) == true|| Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.D) == true )
        {
            anim.SetBool("Run", true);
        }
        else {
            anim.SetBool("Run", false);
        }

        transform.position = transform.position + move * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), rotationSpeed * Time.deltaTime);


        if (Input.GetKeyDown("space") && characterOnGround) 
        {
            jump();
        }
    }

    void jump() 
    {
        anim.SetBool("Jump", true);
        rb.AddForce(new Vector3(0, jumpSize, 0), ForceMode.Impulse);
        characterOnGround = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "JumpOn")
        {
            anim.SetBool("Jump", false);
            characterOnGround = true;
        }
    }
}
