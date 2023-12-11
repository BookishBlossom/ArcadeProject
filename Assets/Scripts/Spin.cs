using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to make the powerup indicator spin
public class Spin : MonoBehaviour
{
    //spin speed variable
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
