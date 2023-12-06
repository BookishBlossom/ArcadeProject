using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public float timeSpawned;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shotTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }

    IEnumerator shotTimer()
    {
        yield return new WaitForSeconds(timeSpawned);
        Destroy(gameObject);
    }
}
