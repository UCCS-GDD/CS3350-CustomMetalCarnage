using UnityEngine;
using System.Collections;

public class JeepMovement : MonoBehaviour
{
    [Range(1, 10)]
    public int speed;
    public float mag;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mag = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *= new Quaternion(0f, 0f, .01f, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation *= new Quaternion(0f, 0f, -.01f, 1);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * speed, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *= new Quaternion(0f, 0f, .01f, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation *= new Quaternion(0f, 0f, -.01f, 1);
            }
        }
    }
}
