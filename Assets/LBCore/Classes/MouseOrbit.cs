using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with Zoom")]
public class MouseOrbit : MonoBehaviour
{

    public Transform target;
    public float defaultDistance = 5.0f;
    public float zoomSpeed = 0.1f;
    private float wantedDistance = 5.0f;
    private float currentDistance = 5.0f;
    public float panSpeed = 0.1f;
    private Vector3 wantedPosition = new Vector3();
    private Vector3 currentPosition = new Vector3();
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zSpeed = 20.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    public Vector2 firstMousePosition;
    public int unlockThreshold = 5;
    public bool spinUnlocked = false;

    public GameObject[] SSCameras;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        currentDistance = defaultDistance;
        wantedDistance = defaultDistance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firstMousePosition = Input.mousePosition;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!spinUnlocked)
            {
                if (Vector2.Distance(Input.mousePosition, firstMousePosition) >= unlockThreshold)
                {
                    spinUnlocked = true;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            spinUnlocked = false;
        }


        if (spinUnlocked)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }

        if (target)
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            wantedDistance = Mathf.Clamp(wantedDistance - Input.GetAxis("Mouse ScrollWheel") * zSpeed, distanceMin, distanceMax);

            currentDistance = Mathf.Lerp(currentDistance, wantedDistance, zoomSpeed);


            Vector3 negDistance = new Vector3(0.0f, 0.0f, -currentDistance);

            wantedPosition = target.position;
            currentPosition = Vector3.Lerp(currentPosition, wantedPosition, panSpeed);

            Vector3 position = rotation * negDistance + currentPosition;


            transform.rotation = rotation;
            transform.position = position;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        }

        foreach (GameObject go in SSCameras)
        {
            go.transform.rotation = this.transform.rotation;
        }
        if (SSCameras.Length > 0)
        {
            SSCameras[0].transform.position = transform.position / 1000;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}