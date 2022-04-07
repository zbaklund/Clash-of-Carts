using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class XRPlayerController : MonoBehaviour
{
    [Header("Behaviour Options")]

    [SerializeField]
    private float maxSpeed = 500.0f;

    [SerializeField]
    private float accelrateInterval = 2.0f;

    [SerializeField]
    private float brakingInterval = 2.0f;

    [SerializeField]
    private float neutralInterval = 1.0f;

    [SerializeField]
    private float rotationSpeed = 2f;

    [Header("Capsule Collider Options")]
    [SerializeField]
    private Vector3 capsuleCenter = new Vector3(0, 1, 0);

    [SerializeField]
    private float capsuleRadius = 0.3f;

    [SerializeField]
    private float capsuleHeight = 1.6f;

    [SerializeField]
    private CapsuleDirection capsuleDirection = CapsuleDirection.YAxis;

    [Header("Camera")]
    public Transform Camera;

    [Header("XR Controller Inputs")]


    private InputDevice LeftController;
    private InputDevice RightController;

    private bool isGrounded;

    private bool buttonPressed;

    private Rigidbody rigidBodyComponent;

    private CapsuleCollider capsuleCollider;

    private List<InputDevice> devices = new List<InputDevice>();

    private float current_speed = 0f;

    private Vector3 current_direction = Vector3.zero;

    public enum CapsuleDirection
    {
        XAxis,
        YAxis,
        ZAxis
    }

    void OnEnable()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rigidBodyComponent.constraints = RigidbodyConstraints.FreezeRotation;
        capsuleCollider.direction = (int)capsuleDirection;
        capsuleCollider.radius = capsuleRadius;
        capsuleCollider.center = capsuleCenter;
        capsuleCollider.height = capsuleHeight;
    }

    void Start()
    {
        GetDevices();
    }

    private void GetDevices()
    {
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if(leftHandDevices.Count == 1)
        {
            LeftController = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", LeftController.name, LeftController.characteristics.ToString()));
        }
        else if(leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if(leftHandDevices.Count == 1)
        {
            RightController = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", RightController.name, RightController.characteristics.ToString()));
        }
        else if(leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }

    void Update()
    {
        if (LeftController == null || RightController == null)
        {
            GetDevices();
        }

        UpdateMovement();

    }

    private void UpdateMovement()
    {

        RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rightRotation);
        Vector3 rightDirection = rightRotation * Vector3.forward;
        //RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPosition);

        LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion leftRotation);
        Vector3 leftDirection = leftRotation * Vector3.forward;
        //LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition);

        Vector3 averageDirection = (rightDirection + leftDirection) / 2;
        averageDirection.y = 0;
        averageDirection.Normalize();

        //Vector3 turnDirection = (averageDirection - current_direction) 

        bool triggerValue;

        // Left trigger pull, deccelerate to 0
        if (LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            current_speed -= brakingInterval;
            if (current_speed < 0f)
            {
                current_speed = 0f;
            }
            
        } // Right trigger pull, accelerate to max speed
        else if (RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            current_speed += accelrateInterval;
            if (current_speed > maxSpeed)
            {
                current_speed = maxSpeed;
            }
        }
        // Neither trigger, slow down at a slower rate
        else
        {
            current_speed -= neutralInterval;
            if (current_speed < 0f)
            {
                current_speed = 0f;
            }
        }

       transform.Translate(Vector3.forward * current_speed * Time.deltaTime);
       transform.rotation = Quaternion.Euler(averageDirection.x * rotationSpeed, 0f, averageDirection.z * rotationSpeed);
       //Camera.rotation = Quaternion.Euler(averageDirection.x * rotationSpeed, 0f, 0f);
       //transform.Rotate(averageDirection * rotationSpeed * Time.deltaTime);


        // Vector2 primary2dValue;

        // InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

        // if (controller.TryGetFeatureValue(primary2DVector, out primary2dValue) && primary2dValue != Vector2.zero)
        // {
        //     Debug.Log("primary2DAxisClick is pressed " + primary2dValue);

        //     var xAxis = primary2dValue.x * speed * Time.deltaTime;
        //     var zAxis = primary2dValue.y * speed * Time.deltaTime;

        //     Vector3 right = transform.TransformDirection(Vector3.right);
        //     Vector3 forward = transform.TransformDirection(Vector3.forward);

        //     transform.position += right * xAxis;
        //     transform.position += forward * zAxis;
        // }
    }
}