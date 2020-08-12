using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    public bool isCollected = false;
    public GameObject slot;
    private Transform target;
    private float speed = 2.0f;
    float slowerSpeed = 1.5f;
    private GameObject pyramidParent;
    public List<GameObject> Pyramids;
    public bool isNextLevel = false;
    Transform center;
    float seconds = 4.5f;

    Renderer r;
    Material mat;
    float intensity = 0.0f;

    Vector3 targetScale;

    public GameObject lightSphere;
    public GameObject starSphere;
    // Start is called before the first frame update
    void Start()
    {
        target = slot.transform;
        center = GameObject.Find("Higher Center").transform;

        r = gameObject.transform.Find("default").gameObject.GetComponent<Renderer>();
        mat = r.material;
        mat.EnableKeyword("_EMISSION");

        targetScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.4f, 0.0f);

        if (isCollected)
        {
            Move(speed, target);
        }
        else if (isNextLevel && !isCollected)
        {
            Move(slowerSpeed, center);
            ShrinkAndGlow();
        }
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(seconds);
        isCollected = false;
        isNextLevel = true;
        AllPyramids();
    }

    private void Move(float speed, Transform target)
    {
        pyramidParent = gameObject.transform.parent.gameObject;
        float step = speed * Time.deltaTime;
        pyramidParent.transform.position = Vector3.MoveTowards(pyramidParent.transform.position, target.position, step);
    }

    void AllPyramids()
    {
        foreach(GameObject pyramid in Pyramids)
        {
            Pyramid pyramidScript = pyramid.gameObject.GetComponent<Pyramid>();

            pyramidScript.isCollected = false;
            pyramidScript.isNextLevel = true;
        }
    }

    void ShrinkAndGlow()
    {
        if (intensity < 1.6f)
        {
            intensity += .007f;
        }
        else
        {
            gameObject.SetActive(false);
            lightSphere.SetActive(true);
            starSphere.SetActive(true);
        }
        mat.SetColor("_EmissionColor", new Color(1.0f, 0.0f, 0.0f, 1.0f) * intensity);

        //gameObject.transform.localScale -= new Vector3(0.007f, 0.007f, 0.007f);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, slowerSpeed * Time.deltaTime);
        
    }
}
