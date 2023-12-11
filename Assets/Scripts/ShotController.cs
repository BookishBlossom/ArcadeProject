using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    //time until shot despawns after spawning
    public float timeSpawned;

    //speed shot moves
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shotTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //moves shot at set speed
        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    //destroys the shot after a set amount of seconds
    IEnumerator shotTimer()
    {
        yield return new WaitForSeconds(timeSpawned);
        Destroy(gameObject);
    }
}
