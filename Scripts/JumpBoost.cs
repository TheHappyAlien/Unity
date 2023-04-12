using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] float jumpBoostForce;
    private Rigidbody2D rb;
    private PlayerController pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            rb = collision.GetComponent<Rigidbody2D>();
            pc = collision.GetComponent<PlayerController>();
            rb.velocity = Vector2.up * jumpBoostForce;
        }
        if (Input.GetButton("Jump"))
        {
            pc.jumpTimeCounter = 0;
        }
    }

}
