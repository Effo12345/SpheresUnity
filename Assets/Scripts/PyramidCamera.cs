using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PyramidCamera : MonoBehaviour
{
    private Transform center;
    private float rotationSpeed = 19;
    Pyramid pyramidScript;
    public GameObject starSphere;

    bool nextLevel = true;
    bool enterSphere = false;

    public string nextLevelName;

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center").GetComponent<Transform>();

        GameObject pyramidParent = gameObject.transform.parent.gameObject;
        GameObject pyramid = pyramidParent.transform.Find("Pyramid").gameObject;
        pyramidScript = pyramid.GetComponent<Pyramid>();


        //pyramidParent = gameObject.transform.parent.gameObject;
        //pyramid = pyramidParent.transform.Find("Pyramid").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(center.transform.position, transform.position);

        if (distance <= 4.0f)
        {
            if (nextLevel)
            {
                nextLevel = false;
                StartCoroutine(pyramidScript.NextLevel());
                StartCoroutine(EnterSphere());
            }

            gameObject.transform.parent = null;

            //float step = 1 * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, center.position, step);

            if (!enterSphere)
            {
                MoveCamera(1.0f, center);
            }

            if (Vector3.Angle(transform.forward, Vector3.forward) <= 90)
            {
                transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            }
        }

        float sphereDistance = Vector3.Distance(starSphere.transform.position, transform.position);

        if (enterSphere)
        {
            MoveCamera(1.0f, starSphere.transform);
            if (sphereDistance <= 0.1f)
            {
                //SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.NextLevel();
            }
        }
    }

    IEnumerator EnterSphere()
    {
        yield return new WaitForSeconds(6);
        enterSphere = true;
    }

    void MoveCamera(float speed, Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void RotateCamera()
    {
        if (gameObject.transform.rotation.x <= 90)
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        else
        {

        }
    }
}
