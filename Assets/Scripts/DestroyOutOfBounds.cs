using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float limit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -limit || gameObject.transform.position.x > limit || gameObject.transform.position.y < -limit || gameObject.transform.position.y > limit)
        {
            Destroy(gameObject);
        }
    }
}
