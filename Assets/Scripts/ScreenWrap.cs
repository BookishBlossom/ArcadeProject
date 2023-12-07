using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    //variables
    public float offset;
    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerShot") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Asteroid"))
        {
            if (direction == "x" || direction == "X")
            {
                other.transform.position = new Vector3((-other.transform.position.x) + offset, other.transform.position.y, -1.5f);

            }
            else if (direction == "y" || direction == "Y")
            {
                other.transform.position = new Vector3(other.transform.position.x, (-other.transform.position.y) + offset, -1.5f);

            }
        }
    }
}
