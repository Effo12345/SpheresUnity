using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject camera;

    [SerializeField] float speed = 1.0f;

    public int pyramidsNeedToCollect;
    private int pyramidsCollected = 0;

    bool isNextLevel = false;

    Transform center;
    float lowerBounds = 5.0f;
    bool sendToCenter = false;
    bool zeroAngularVelocity = false;
    float moveSpeed = 15.0f;

    [HideInInspector]
    public static bool isPaused = false;
    public GameObject pauseCanvas;

    public bool isSlow;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        camera = GameObject.Find("Main Camera");

        center = GameObject.Find("Center").GetComponent<Transform>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.y < -lowerBounds)
        {
            sendToCenter = true;
        }

        float distance = Vector3.Distance(center.position, transform.position);
        if (sendToCenter && distance != 0)
        {
            playerRB.useGravity = false;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, center.position, step);
            zeroAngularVelocity = true;
        }
        else
        {
            if (zeroAngularVelocity)
            {
                playerRB.angularVelocity = Vector3.zero;
                zeroAngularVelocity = false;
            }
            else
            {
                playerRB.useGravity = true;
                sendToCenter = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Menu();
        }
    }

    private void Move()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sidewaysInput = Input.GetAxis("Horizontal");

        if ((Input.GetKey(KeyCode.LeftControl) | Input.GetKey(KeyCode.RightControl) && !isNextLevel))
        {
            speed = .5f;

            isSlow = true;
        }
        else if ((Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift)) && !isNextLevel)
        {
            speed = 2.0f;
        }
        else
        {
            speed = 1.0f;

            isSlow = false;
        }

        if (!isNextLevel && !isPaused)
        {
            playerRB.AddForce(camera.transform.forward * forwardInput * speed);
            playerRB.AddForce(camera.transform.right * sidewaysInput * speed);
        }
        else
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pyramid"))
        {
            other.gameObject.GetComponent<Animator>().enabled = false;
            Pyramid pyramidScript = other.gameObject.GetComponent<Pyramid>();
            pyramidScript.isCollected = true;
            pyramidsCollected++;
            //other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            Destroy(other.gameObject.GetComponent<BoxCollider>());

            GameObject pyramidParent = other.transform.parent.gameObject;
            BeaconBeam beaconBeam = pyramidParent.transform.Find("Beacon").gameObject.GetComponent<BeaconBeam>();
            beaconBeam.PyramidIsCollected();

            if (pyramidsCollected == pyramidsNeedToCollect)
            {
                isNextLevel = true;
                GameObject pyramidCamera = pyramidParent.transform.Find("Pyramid Camera").gameObject;
                camera.SetActive(false);
                pyramidCamera.SetActive(true);

                Serialization serialization = GameObject.Find("Serializer").GetComponent<Serialization>();
                serialization.NextLevel();
            }
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Serialization.nextBuildIndex, LoadSceneMode.Single);
    }

    void Menu()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
        Cursor.visible = true;
    }

    public void UndoMenu()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Cursor.visible = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            gameObject.transform.parent = other.transform.parent.transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            gameObject.transform.parent = null;
        }
    }
}