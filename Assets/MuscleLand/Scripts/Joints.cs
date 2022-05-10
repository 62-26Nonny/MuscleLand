using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mediapipe;

public class Joints : MonoBehaviour
{
    public enum pose {
        NOSE = 0,
        L_EYE_INNER = 1,
        L_EYE = 2,
        L_EYE_OUTER = 3,
        R_EYE_INNER = 4,
        R_EYE = 5,
        R_EYE_OUTER = 6,
        L_EAR = 7,
        R_EAR = 8,
        L_MOUTH = 9,
        R_MOUTH = 10,
        L_SHOULDER = 12,
        R_SHOULDER = 11,
        L_ELBOW = 14,
        R_ELBOW = 13,
        L_WRIST = 16,
        R_WRIST = 15,
        L_PINKY = 17,
        R_PINKY = 18,
        L_INDEX = 19,
        R_INDEX = 20,
        L_THUMB = 21,
        R_THUMB = 22,
        L_HIP = 24,
        R_HIP = 23,
        L_KNEE = 26,
        R_KNEE = 25,
        L_ANKLE = 28,
        R_ANKLE = 27,
        L_HEEL = 30,
        R_HEEL = 29,
        L_FINDEX = 32,
        R_FINDEX = 31
    }

    public GameObject model;
    public GameObject head;
    public GameObject nose;
    public GameObject initPoint;
    public GameObject indexL;
    public GameObject indexR;
    // public GameObject pinkyL;
    // public GameObject pinkyR;
    // public GameObject thumbL;
    // public GameObject thumbR;
    public GameObject wristL;
    public GameObject wristR;
    public GameObject elbowL;
    public GameObject elbowR;
    public GameObject shoulderL;
    public GameObject shoulderR;
    public GameObject hipL;
    public GameObject hipR;
    public GameObject kneeL;
    public GameObject kneeR;
    public GameObject ankleL;
    public GameObject ankleR;
    // public GameObject heelL;
    // public GameObject heelR;
    // public GameObject footIndexL;
    // public GameObject footIndexR;

    private void Update() 
    {
        if (MediaPipeValues.poseLandmarks != null) 
        {
            NormalizedLandmark head_tracker = MediaPipeValues.poseLandmarks.Landmark[(int)pose.NOSE];

            NormalizedLandmark L_index = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_INDEX];
            NormalizedLandmark R_index = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_INDEX];

            NormalizedLandmark L_pinky = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_PINKY];
            NormalizedLandmark R_pinky = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_PINKY];

            NormalizedLandmark L_thumb = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_THUMB];
            NormalizedLandmark R_thumb = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_THUMB];

            NormalizedLandmark L_wrist = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_WRIST];
            NormalizedLandmark R_wrist = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_WRIST];

            NormalizedLandmark L_elbow = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_ELBOW];
            NormalizedLandmark R_elbow = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_ELBOW];

            NormalizedLandmark L_shoulder = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_SHOULDER];
            NormalizedLandmark R_shoulder = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_SHOULDER];
            
            NormalizedLandmark L_hip = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_HIP];
            NormalizedLandmark R_hip = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_HIP];

            NormalizedLandmark L_knee = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_KNEE];
            NormalizedLandmark R_knee = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_KNEE];

            NormalizedLandmark L_ankle = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_ANKLE];
            NormalizedLandmark R_ankle = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_ANKLE];

            NormalizedLandmark L_heel = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_ANKLE];
            NormalizedLandmark R_heel = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_ANKLE];

            NormalizedLandmark L_foot_index = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_FINDEX];
            NormalizedLandmark R_foot_index = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_FINDEX];

            AdjustJoint(indexL, (1 - L_index.X) * 1200 - 600, (1 - L_index.Y) * 540 - 400, L_index.Z * 200 - 250);
            AdjustJoint(indexR, (1 - R_index.X) * 1200 - 600, (1 - R_index.Y) * 540 - 400, R_index.Z * 200 - 250);

            // AdjustJoint(pinkyL , L_pinky.X * 1200 - 600, (1 - L_pinky.Y) * 540 - 540, L_pinky.Z * 250);
            // AdjustJoint(pinkyR , R_pinky.X * 1200 - 600, (1 - R_pinky.Y) * 540 - 540, R_pinky.Z * 250);

            // AdjustJoint(thumbL , L_thumb.X * 1200 - 600, (1 - L_thumb.Y) * 540 - 540, L_thumb.Z * 250);
            // AdjustJoint(thumbR , R_thumb.X * 1200 - 600, (1 - R_thumb.Y) * 540 - 540, R_thumb.Z * 250);

            AdjustJoint(wristL, (1 - L_wrist.X) * 1200 - 600, (1 - L_wrist.Y) * 540 - 400, L_wrist.Z * 200 - 250);
            AdjustJoint(wristR, (1 - R_wrist.X) * 1200 - 600, (1 - R_wrist.Y) * 540 - 400, R_wrist.Z * 200 - 250);

            AdjustJoint(elbowL, (1 - L_elbow.X) * 1200 - 600, (1 - L_elbow.Y) * 540 - 400, L_elbow.Z * 200 - 250);
            AdjustJoint(elbowR, (1 - R_elbow.X) * 1200 - 600, (1 - R_elbow.Y) * 540 - 400, R_elbow.Z * 200 - 250);

            AdjustJoint(shoulderL , (1 - L_shoulder.X) * 1200 - 600, (1 - L_shoulder.Y) * 540 - 400, L_shoulder.Z * 250);
            AdjustJoint(shoulderR , (1 - R_shoulder.X) * 1200 - 600, (1 - R_shoulder.Y) * 540 - 400, R_shoulder.Z * 250);

            AdjustJoint(hipL , (1 - L_hip.X) * 1200 - 600, (1 - L_hip.Y) * 540 - 400, L_hip.Z * 250);
            AdjustJoint(hipR , (1 - R_hip.X) * 1200 - 600, (1 - R_hip.Y) * 540 - 400, R_hip.Z * 250);

            AdjustJoint(ankleL, (1 - L_ankle.X) * 1200 - 600, (1 - L_ankle.Y) * 540 - 400, L_ankle.Z * 200 - 250);
            AdjustJoint(ankleR, (1 - R_ankle.X) * 1200 - 600, (1 - R_ankle.Y) * 540 - 400, R_ankle.Z * 200 - 250);

            AdjustJoint(kneeL, (1 - L_knee.X) * 1200 - 600, (1 - L_knee.Y) * 540 - 400, L_knee.Z * 200 - 250);
            AdjustJoint(kneeR, (1 - R_knee.X) * 1200 - 600, (1 - R_knee.Y) * 540 - 400, R_knee.Z * 200 - 250);
            
            // AdjustJoint(heelL , L_heel.X * 1200 - 600, (1 - L_heel.Y) * 540 - 540, L_heel.Z * 250);
            // AdjustJoint(heelR , R_heel.X * 1200 - 600, (1 - R_heel.Y) * 540 - 540, R_heel.Z * 250);

            // AdjustJoint(footIndexL , L_foot_index.X * 1200 - 600, (1 - L_foot_index.Y) * 540 - 540, L_foot_index.Z * 250);
            // AdjustJoint(footIndexR , R_foot_index.X * 1200 - 600, (1 - R_foot_index.Y) * 540 - 540, R_foot_index.Z * 250);

            AdjustJoint(initPoint, head_tracker.X * 1200 - 600, (1 - head_tracker.Y) * 540 - 930, head_tracker.Z * 200 - 125);
            AdjustJoint(nose, head_tracker.X * 1200 - 600, (1 - head_tracker.Y) * 540 - 400, head_tracker.Z * 200 - 250);

            float heightModel = Mathf.Abs(head.transform.position.y - initPoint.transform.position.y);
            float heightTracker = Mathf.Abs(nose.transform.position.y - initPoint.transform.position.y);
            
            if (heightModel < heightTracker)
            {
                while (heightModel <= heightTracker)
                {
                    model.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                    heightModel = Mathf.Abs(head.transform.position.y - initPoint.transform.position.y);
                }
            }
            else if (heightModel > heightTracker)
            {
                while (heightModel >= heightTracker)
                {
                    model.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    heightModel = Mathf.Abs(head.transform.position.y - initPoint.transform.position.y);
                }
            }

            // Debug.Log("Model: " + heightModel);
            // Debug.Log("Tracker: " + heightTracker);
        }
    }

    public void AdjustJoint(GameObject joint, double posX, double posY, double posZ)
    {
        joint.transform.position = new Vector3((float)posX, (float)posY, (float)posZ);
    }
}
