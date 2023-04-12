using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpikes : MonoBehaviour
{
    [SerializeField] GameObject Dust;
    [SerializeField] GameObject spikeShooter;
    private Rigidbody2D rb;
    private float positionX;
    private float positionY;
    private float speed;
    private Transform trans;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        positionX = spikeShooter.GetComponent<SpikeShootingScript>().positionX;
        positionY = spikeShooter.GetComponent<SpikeShootingScript>().positionY;
        speed = spikeShooter.GetComponent<SpikeShootingScript>().speed;
        trans = spikeShooter.GetComponent<Transform>();
    }

    private void Update()
    {
        if (this.gameObject.name == "ShotSpikes(Clone)")
        {
            rb.velocity = new Vector2(positionX * speed, positionY * speed);
            transform.rotation = trans.rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Solid"))
        {
            Instantiate(Dust, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
