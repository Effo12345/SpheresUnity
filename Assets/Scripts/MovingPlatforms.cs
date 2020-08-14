using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Vector3 point1;
    public Vector3 point2;

    bool moveToPoint2 = true;

    float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        point1 = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPoint1 = ReturnDistance(point1);
        float distanceToPoint2 = ReturnDistance(point2);

        if (moveToPoint2)
        {
            if (distanceToPoint2 != 0.0f)
            {
                Move(point2);
            }
            else
            {
                StartCoroutine(Wait());
            }
            
        }
        else
        {
            moveToPoint2 = false;
        }


        if (!moveToPoint2)
        {
            if (distanceToPoint1 != 0.0f)
            {
                Move(point1);
            }
            else
            {
                StartCoroutine(Wait());
            }
        }
        else
        {
            moveToPoint2 = true;
        }
    }

    private void Move(Vector3 target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    float ReturnDistance(Vector3 point)
    {
        float distanceToPoint = Vector3.Distance(point, transform.position);
        return distanceToPoint;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }
}
