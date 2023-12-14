using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    //how far the moved object is moved from the exit point
    public float offset;

    // for if the barrier is at the top/bottom or left/right
    public string direction;

    //
    private void OnTriggerEnter(Collider other)
    {
        //limits objects moved to the player, the player's shots, saucers, saucer shots, and asteroids
        if (!other.gameObject.CompareTag("Shield") && !other.gameObject.CompareTag("PowerUp"))
        {
            // if barrier's on the x axis the object's x axis is flipped
            if (direction == "x" || direction == "X")
            {
                other.transform.position = new Vector3((-other.transform.position.x) + offset, other.transform.position.y, -1.5f);

            }
            // if barrier's on the y axis the object's y axis is flipped
            else if (direction == "y" || direction == "Y")
            {
                other.transform.position = new Vector3(other.transform.position.x, (-other.transform.position.y) + offset, -1.5f);

            }
        }
    }
}
