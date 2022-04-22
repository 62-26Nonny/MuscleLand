using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mediapipe;
using Game.Timer;
public class MediaPipeManager : MonoBehaviour
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
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject countdown;
    public Image[] frame_status;
    public Timer Timer_script;
    private bool firstLoad;

    private void Start() {
        firstLoad = true;
    }

    void Update()
    {
        if (MediaPipeValues.poseLandmarks != null) {
            loading.SetActive(false);
            if (firstLoad) {
                countdown.SetActive(true);
                if (isInFrame()){
                    StartCoroutine(Timer_script.StartCountdown());
                    firstLoad = false;
                }
            }
            
            NormalizedLandmark L_shoulder = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_SHOULDER];
            NormalizedLandmark R_shoulder = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_SHOULDER];
            
            NormalizedLandmark L_elbow = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_ELBOW];
            NormalizedLandmark R_elbow = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_ELBOW];
            
            NormalizedLandmark L_wrist = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_WRIST];
            NormalizedLandmark R_wrist = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_WRIST];

            NormalizedLandmark L_hip = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_HIP];
            NormalizedLandmark R_hip = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_HIP];

            NormalizedLandmark L_knee = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_KNEE];
            NormalizedLandmark R_knee = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_KNEE];

            NormalizedLandmark L_ankle = MediaPipeValues.poseLandmarks.Landmark[(int)pose.L_ANKLE];
            NormalizedLandmark R_ankle = MediaPipeValues.poseLandmarks.Landmark[(int)pose.R_ANKLE];

            switch (DungeonValues.Dungeon_displayname){
                case "Squat":
                case "Rising Knee":
                    Counter.L_elbow_angle = get3DAngle(L_shoulder, L_elbow, L_wrist);
                    Counter.R_elbow_angle = get3DAngle(R_shoulder, R_elbow, R_wrist);

                    Counter.L_shoulder_angle = get3DAngle(L_elbow, L_shoulder, L_hip);
                    Counter.R_shoulder_angle = get3DAngle(R_elbow, R_shoulder, R_hip);

                    Counter.L_hip_angle = get3DAngle(L_shoulder, L_hip, L_knee);
                    Counter.R_hip_angle = get3DAngle(R_shoulder, R_hip, R_knee);

                    Counter.L_knee_angle = get3DAngle(L_hip, L_knee, L_ankle);
                    Counter.R_knee_angle = get3DAngle(R_hip, R_knee, R_ankle);
                    break;
                case "Jumping Jack":
                    Counter.L_elbow_angle = get2DAngle(L_shoulder, L_elbow, L_wrist);
                    Counter.R_elbow_angle = get2DAngle(R_shoulder, R_elbow, R_wrist);

                    Counter.L_shoulder_angle = get2DAngle(L_elbow, L_shoulder, L_hip);
                    Counter.R_shoulder_angle = get2DAngle(R_elbow, R_shoulder, R_hip);

                    Counter.L_hip_angle = get2DAngle(L_shoulder, L_hip, L_knee);
                    Counter.R_hip_angle = get2DAngle(R_shoulder, R_hip, R_knee);

                    Counter.L_knee_angle = get2DAngle(L_hip, L_knee, L_ankle);
                    Counter.R_knee_angle = get2DAngle(R_hip, R_knee, R_ankle);
                    break;
            }

            Counter.counter();
        }

    }

    public int get3DAngle(NormalizedLandmark start, NormalizedLandmark mid, NormalizedLandmark end){
        var b = new Vector3(mid.X, mid.Y, mid.Z);
        var ba = (new Vector3(start.X, start.Y, start.Z)) - b;
        var bc = (new Vector3(end.X, end.Y, end.Z)) - b;

        var cosine = Vector3.Dot(ba, bc) / (ba.magnitude * bc.magnitude);
        var angle = Mathf.Acos(cosine);
        var result = Mathf.Rad2Deg * angle;

        return (int)result;
    }

    public int get2DAngle(NormalizedLandmark start, NormalizedLandmark mid, NormalizedLandmark end){
        double radians = Math.Atan2(end.Y - mid.Y, end.X - mid.X) - Math.Atan2(start.Y - mid.Y, start.X - mid.X);

        int angle = (int)Math.Abs(radians * 180/Math.PI);

        if (angle > 180){
            angle = 360 - angle;
        }

        return angle;
    }

    public bool isInFrame(){
        foreach(int index in Enum.GetValues(typeof(pose))){
            NormalizedLandmark landmark = MediaPipeValues.poseLandmarks.Landmark[index];
            float posX = landmark.X * 1200 - 600;
            float posY = landmark.Y * 540 - 270;
            if (posX < -530 || posX > 530 || posY < -530 || posY > 530){
                frame_status[0].color = new Color32(255,0,0,255);
                frame_status[1].color = new Color32(255,0,0,255);
                return false;
            }
        }
        frame_status[0].color = new Color32(0,255,0,255);
        frame_status[1].color = new Color32(0,255,0,255);
        return true;
    }

}
