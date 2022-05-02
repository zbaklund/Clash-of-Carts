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
    private float l_triggerValue;
    private bool r_pressed;
    private bool l_pressed;

    public WheelCollider frontLeft, frontRight;
    public WheelCollider backLeft, backRight;
    public float maxSteerAngle;
    public float motorforce;
    public float turnIntensity;
    public float acceleration;
    public float brakeForce;

    private InputDevice LeftController;
    private InputDevice RightController;
    private bool detectedInputDevices = false;


    public void GetInput(){
        bool r_success = RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPosition);
        bool l_success = LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition);
        if (!r_success || !l_success) {
            Debug.Log("Lost Connection to Devices!");
            detectedInputDevices = false;
        }
        
        rightControllerZ = rightPosition.z;
        leftControllerZ = leftPosition.z;

        
        LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out l_pressed);
        RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out r_pressed);
        LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out l_triggerValue);
        RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out r_triggerValue);
    }

    public void Steer(){
        float diff_z = Math.Abs(rightControllerZ - leftControllerZ);
        steeringAngle = maxSteerAngle * (diff_z * turnIntensity);
        if(rightControllerZ > leftControllerZ){
            steeringAngle = -1.0F * steeringAngle;
        }
        frontLeft.steerAngle = steeringAngle;
        frontRight.steerAngle = steeringAngle;
    }

    public void Accelerate(){        

        l_triggerValue = l_triggerValue * -1F;
        float verticalInput = l_triggerValue + r_triggerValue;
        backRight.motorTorque = -1.0F * verticalInput * motorforce;
        backLeft.motorTorque = -1.0F * verticalInput * motorforce;

        // if(l_pressed && r_pressed){
        //     return;
        
        // if(!l_pressed && !r_pressed){
        //     backLeft.motorTorque -= acceleration;
        //     backRight.motorTorque -= acceleration;
        //     if(backLeft.motorTorque < 0){
        //         backLeft.motorTorque = 0;
        //     }
        //     if(backRight.motorTorque < 0){
        //         backRight.motorTorque = 0;
        //     }
        // }

        // //scale torque up to the degree of trigger pull x max speed
        // }else if(r_pressed){
        //     backLeft.motorTorque += r_triggerValue * acceleration;
        //     backRight.motorTorque += r_triggerValue * acceleration;
        //     if(backLeft.motorTorque > r_triggerValue * motorforce){
        //         backLeft.motorTorque = r_triggerValue * motorforce;
        //     }
        //     if(backRight.motorTorque > r_triggerValue * motorforce){
        //         backRight.motorTorque = r_triggerValue *  motorforce;
        //     }
        
        // //scale torque down to the degree of trigger pull x max speed
        // }else if(l_pressed){
        //     backLeft.motorTorque -= r_triggerValue * acceleration;
        //     backRight.motorTorque -= r_triggerValue * acceleration;
        //     if(backLeft.motorTorque < -1.0F * l_triggerValue * motorforce){
        //         backLeft.motorTorque = -1.0F * l_triggerValue * motorforce;
        //     }
        //     if(backRight.motorTorque < -1.0F * l_triggerValue * motorforce){
        //         backRight.motorTorque = -1.0F * l_triggerValue *  motorforce;
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
            detectedInputDevices = true;
        }
        else if(leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if(rightHandDevices.Count == 1)
        {
            RightController = rightHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", RightController.name, RightController.characteristics.ToString()));
        }
        else if(rightHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }

    private void FixedUpdate(){
        if (!detectedInputDevices){
            GetDevices();
        } else {
            GetInput();
            Steer();
            Accelerate();
        }
    }

    

}
