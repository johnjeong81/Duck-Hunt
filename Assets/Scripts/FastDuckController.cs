using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDuckController : MonoBehaviour
{
    public float speed = 6f;
    public Vector2 flightDirection = new Vector2(1, 1);

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveDuck();
        RandomDirection();
    }

    void MoveDuck()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += flightDirection.x * speed * Time.deltaTime;
        newPosition.y += flightDirection.y * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void RandomDirection()
    {
        if (flightDirection.x > 0 && flightDirection.y > 0)
            anim.Play("FastDuckFlyUp");
        if (flightDirection.x < 0 && flightDirection.y > 0)
            anim.Play("FastDuckFlyUpOtherWay");
        if (flightDirection.x > 0 && flightDirection.y < 0)
            anim.Play("FastDuckFly");
        if (flightDirection.x < 0 && flightDirection.y < 0)
            anim.Play("FastDuckFlyOtherWay");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Boundary"))
        {
            flightDirection.y = -flightDirection.y;
        }

        else if (col.gameObject.CompareTag("BoundaryNew"))
        {
            flightDirection.x = -flightDirection.x;
        }
    }
}
