using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float up = 300.0f;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 jump = new Vector3(0.0f, up, 0.0f);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(movement * speed);
        }

        if (Input.GetKey("space") && rb.transform.position.y <= 0.0f)
        {
            rb.AddForce(jump);
        }

    }

}

