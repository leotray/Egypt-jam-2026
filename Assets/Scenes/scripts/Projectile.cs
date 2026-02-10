using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifetime;
    public Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyProjectile", lifetime);
    }
    public void Update()
    {
        rb.AddForce(transform.right * speed);
    }



    public void destroyProjectile()
    {
        Destroy(gameObject);
    }
}
