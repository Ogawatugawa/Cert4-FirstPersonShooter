using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    public float speed = 10f;
    public Transform line;
    public GameObject effectPrefab;

    private Rigidbody rigid;

    // Start is called before the first frame update

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.magnitude > 0f)
        {
            // Rigidbody velocity is a Vector3 for speed in a direction, so we use it to tell the Line Renderer to rotate in the direction the bullet will be traveling
            line.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (effectPrefab)
        {
            // Get contact point
            // ContactPoint is a built-in variable, and contacts is an array holding everything the collider has hit
            ContactPoint contact = collision.contacts[0];
            // Spawn the effect and rotate to contact normal
            // We'll need to use Quaternion.LookRotation to turn contact.normal from Vector3 to a Quaternion
            Instantiate(effectPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        }

        //Destroy bullet
        Destroy(gameObject);
    }

    public override void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        // AddForce by default will increase force over time, so we change ForceMode to Impulse to make it apply the force instantly
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        // Move our Game Object 'line' to the position of lineOrigin
        line.position = lineOrigin;
    }
}
