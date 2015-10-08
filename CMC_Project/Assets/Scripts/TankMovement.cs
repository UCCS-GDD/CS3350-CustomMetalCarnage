using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour
{
    [Range(1, 10)]
    public int speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.W))
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
        if(Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * speed, ForceMode2D.Impulse);
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation *= new Quaternion(0f, 0f, -.01f, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation *= new Quaternion(0f, 0f, .01f, 1);
            }
        }
        if(Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.S) != true)
        {
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
