using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySpawner : MonoBehaviour
{
    [SerializeField]
    private Firefly[] fireflies;

    private float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Firefly flyInstance = Instantiate(fireflies[Random.Range(0, fireflies.Length)],
            new Vector3(transform.position.x, transform.position.y + Random.Range(-2, 2), transform.position.z), Quaternion.identity);

        int power = Random.Range(2, 5);
        flyInstance.Setup(power, transform.right);

        yield return new WaitForSeconds(Random.Range(2,6));

        StartCoroutine(Spawn());
    }
}
