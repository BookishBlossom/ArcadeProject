using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //variables
    public float boostForce;
    public float turnSpeed;
    private float horizontalInput;
    private Rigidbody playerRb;
    private Vector3 lookDirection;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = new Vector3(transform.rotation.x, 0, 0);
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetButtonDown("Boost"))
        {
            playerRb.AddForce(lookDirection * boostForce, ForceMode.Impulse);
        }
    }
}
