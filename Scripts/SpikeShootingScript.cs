using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public class SpikeShootingScript : MonoBehaviour
{

    public float speed;
    [SerializeField] private GameObject shotSpikes;
    [SerializeField] private Transform spikeSpawner;
    [SerializeField] private float frequency;
    public float positionX;
    public float positionY;

    private void Start()
    {
        positionX = spikeSpawner.transform.position.x - transform.position.x;
        positionY = spikeSpawner.transform.position.y - transform.position.y;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(frequency);
        Instantiate(shotSpikes, spikeSpawner.transform.position, Quaternion.identity);
        StartCoroutine(Wait());
    }
}       
