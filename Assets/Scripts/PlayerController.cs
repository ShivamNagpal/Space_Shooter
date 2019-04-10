using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundry boundry;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private Rigidbody rb;
    private AudioSource asrc;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        asrc = this.GetComponent<AudioSource>();
        nextFire = 0f;
    }

    private void Update()
    {
        if (GameController.Paused)
        {
            return;
        }

        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.J))
        {
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                this.asrc.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundry.xMin, boundry.xMax),
                0,
                Mathf.Clamp(rb.position.z, boundry.zMin, boundry.zMax)
            );
        rb.rotation = Quaternion.Euler(0, 0, -1 * rb.velocity.x * tilt);
    }
}
