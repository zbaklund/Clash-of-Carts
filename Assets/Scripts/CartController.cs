using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class CartController : MonoBehaviour
{
    // difference in z determines turn angle
    private float steeringAngle;
    private float rightControllerZ;
    private float leftControllerZ;
    private float r_triggerValue;
    private bool l_triggerValue;

    public WheelCollider frontLeft, frontRight;
    public WheelCollider backLeft, backRight;
    public float maxSteerAngle = 30;
    public float motorforce = 10;
    public float turnIntensity = 1;
    public float acceleration = 0.5F;

    private InputDevice LeftController;
    private InputDevice RightController;

    public GameObject cube;


    public void GetInput(){
        RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPosition);
        LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition);
        
        rightControllerZ = rightPosition.z;
        leftControllerZ = leftPosition.z;

        
        LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out l_triggerValue);
    }

    public void Steer(){
        float diff_z = Math.Abs(rightControllerZ - leftControllerZ);
        steeringAngle = maxSteerAngle * (diff_z * turnIntensity);
        frontLeft.steerAngle = steeringAngle;
        frontRight.steerAngle = steeringAngle;
    }

    public void Accelerate(){
        RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out r_triggerValue);
        if(r_triggerValue != 0){
            var cubeRenderer = cube.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", Color.red);
        }
        if(Input.GetKeyDown("space")){
            var cubeRenderer = cube.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", Color.red);
        }
        backLeft.motorTorque = r_triggerValue * motorforce;
        backRight.motorTorque = r_triggerValue * motorforce;
        // if(RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out r_triggerValue) && r_triggerValue){
        //     backLeft.motorTorque += acceleration;
        //     backRight.motorTorque += acceleration;

        //     if(backLeft.motorTorque > motorforce){
        //         backRight.motorTorque = motorforce;
        //         backLeft.motorTorque = motorforce;
        //     }
        // }
        if(l_triggerValue){
            if(backLeft.motorTorque <= 0){
                backLeft.motorTorque -= acceleration;
                backRight.motorTorque -= acceleration;
            }else{
                backLeft.motorTorque = backLeft.motorTorque < 2 ? 0 : backLeft.motorTorque - 2;
                backRight.motorTorque = backLeft.motorTorque;
            }
            var cubeRenderer = cube.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", Color.blue);
        }
        // }else if (r_triggerValue){
        //     backLeft.motorTorque += acceleration;
        //     backRight.motorTorque += acceleration;

        //     if(backLeft.motorTorque > motorforce){
        //         backRight.motorTorque = motorforce;
        //         backLeft.motorTorque = motorforce;
        //     }
        // }
        
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

    private void FixedUpdate(){
        if (LeftController == null || RightController == null){
            GetDevices();
        }

        GetInput();
        //Steer();
        Accelerate();
    }

    

}
