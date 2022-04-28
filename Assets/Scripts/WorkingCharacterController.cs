// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.XR;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(CapsuleCollider))]
// public class XRPlayerController : MonoBehaviour
// {

//     private InputDevice LeftController;
//     private InputDevice RightController;

//     void Start()
//     {
//         GetDevices();
//     }

//     private void GetDevices()
//     {
//         var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
//         UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

//         if(leftHandDevices.Count == 1)
//         {
//             LeftController = leftHandDevices[0];
//             Debug.Log(string.Format("Device name '{0}' with role '{1}'", LeftController.name, LeftController.characteristics.ToString()));
//         }
//         else if(leftHandDevices.Count > 1)
//         {
//             Debug.Log("Found more than one left hand!");
//         }

//         var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
//         UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

//         if(leftHandDevices.Count == 1)
//         {
//             RightController = rightHandDevices[0];
//             Debug.Log(string.Format("Device name '{0}' with role '{1}'", RightController.name, RightController.characteristics.ToString()));
//         }
//         else if(leftHandDevices.Count > 1)
//         {
//             Debug.Log("Found more than one left hand!");
//         }
//     }

//     void Update()
//     {
//         if (LeftController == null || RightController == null)
//         {
//             GetDevices();
//         }

//         UpdateMovement();

//     }

//     void UpdateMovement()
//     {
//         RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 rightPosition);
//         LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 leftPosition);

//         Vector3 center = (rightPosition + leftPosition) / 2;
//         Vector3 direction;
//         direction.x = rightPosition.x - leftPosition.x;
//         direction.y = 0;
//         direction.z = rightPosition.z - leftPosition.z;

//         //float slope = (rightPosition.x - leftPosition.x) / (rightPosition.z - leftPosition.z);

//         //Debug.DrawRay(center, direction, Color.green, 1, false);


//         if(RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue))
//         {

//         }
//     }
// }