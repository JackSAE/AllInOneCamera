using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class AllInOneCameraController : MonoBehaviour
{

    #region General Settings
    public GameObject target;
    public Transform camTransform;
    #endregion



    #region First Person Camera Variables
    public static bool firstPersonCamera = false;
    public static bool firstPersonCameraFoldout = false;
    public float firstPersonCameraYOffset = 1.2f;

    public float lookSmooth = 0.09f;
    public Vector3 offSetFromPlayerFirstPerson = new Vector3(0, 6, -8);

    Vector3 destination = Vector3.zero;
    public float rotateVelo = 0;

    #endregion



    #region 3rd Person Camera Variables
    public static bool thirdPersonCamera = false;
    public static bool thirdPersonCameraFoldout = false;

    public  bool rotateCamera = true;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    public float damping = 5;
    public float rotationSpeed = 3;

    public bool collisionsOn = true;
    public bool moveWithMouse = false;

    #endregion



    #region TopDown Camera Variables
    public static bool topDownCamera = false;
    public static bool topDownCameraFoldout = false;
    #endregion



    #region MMO Camera Variables
    public static bool mMOCamera = false;
    public static bool mMOCameraFoldout = false;

    public const float Y_ANGLE_MIN = 2.0f;
    public const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    

    public Camera cam;

    public float distance = 10;
    public float currentX;
    public float currentY;
    public float sensitivtyX;
    public float sensitivityY;
    #endregion


    void Start()
    {
        if (target == null)
            Debug.LogError("Camera Needs a Target");
    }


    public void Update()
    {
        camTransform = transform;

        if (mMOCamera)
        {
            cam = Camera.main;

            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        }

    }


    void LateUpdate()
    {
        //Third Person Camera
        if(thirdPersonCamera)
        {
            LookAtTargetThirdPerson();

            MoveToTargetThirdPerson();
        }

        //First Person Camera
        if(firstPersonCamera)
        {

            //MoveTo
            MoveToTargetFirstPeron();

            //Rotate Camera
            LookAtTargetFirstPeron();
        }

        //MMO Camera
        if(mMOCamera)
        {
            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            }
            else
            {
            //Moving with Mouse direction
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
            }



        }

        //Top Down Camera
        if(topDownCamera)
        {

        }

    }

    void LookAtTargetThirdPerson()
    {
        if(rotateCamera == true)
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - camTransform.position);
            camTransform.rotation = Quaternion.Slerp(camTransform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if(Input.GetMouseButton(1))
            {
                transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            }
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - camTransform.position);
            camTransform.rotation = Quaternion.Slerp(camTransform.rotation, rotation, Time.deltaTime * damping);
        }

    }

    void MoveToTargetThirdPerson()
    {
        Vector3 getTrasform = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, target.transform.position.z + zOffset);
        camTransform.position = Vector3.Lerp(camTransform.position, getTrasform, Time.deltaTime );
    }

    void LookAtTargetFirstPeron()
    {
        //FirstPersonCamera
        float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.transform.eulerAngles.y, ref rotateVelo, lookSmooth);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
    }

    void MoveToTargetFirstPeron()
    {
        //First Person Camera
        if(target)
        {
            destination = target.transform.position + Vector3.up * firstPersonCameraYOffset;
            transform.position = destination;
        }
        else
        {
            destination = target.transform.position + offSetFromPlayerFirstPerson * firstPersonCameraYOffset;
            transform.position = destination;
        }
    }

}


