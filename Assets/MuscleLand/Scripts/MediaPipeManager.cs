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

    [SerializeField] public Text L_Shoulder_Text;
    [SerializeField] public Text R_Shoulder_Text;
    [SerializeField] public Text L_Elbow_Text;
    [SerializeField] public Text R_Elbow_Text;
    [SerializeField] public Text L_Wrist_Text;
    [SerializeField] public Text R_Wrist_Text;
    [SerializeField] public Text L_Hip_Text;
    [SerializeField] public Text R_Hip_Text;
    [SerializeField] public Text L_Knee_Text;
    [SerializeField] public Text R_Knee_Text;
    [SerializeField] public Text L_Ankle_Text;
    [SerializeField] public Text R_Ankle_Text;

    [SerializeField] public Text L_Shoulder_Text_Box;
    [SerializeField] public Text R_Shoulder_Text_Box;
    [SerializeField] public Text L_Elbow_Text_Box;
    [SerializeField] public Text R_Elbow_Text_Box;
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject countdown;
    public GameObject Timer;
    private Timer Timer_script;

    private bool firstLoad;

    private void Start() {
        firstLoad = true;
        Timer_script = Timer.GetComponent<Timer>();
    }

    void Update()
    {
        if (MediaPipeValues.poseLandmarks != null) {
            loading.SetActive(false);
            if (firstLoad) {
                countdown.SetActive(true);
                StartCoroutine(Timer_script.StartCountDown());
                firstLoad = false;
            }
            

            // Debug.Log(MediaPipeValues.poseLandmarks.Landmark);

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

            SquatCounter.L_elbow_angle = get3DAngle(L_shoulder, L_elbow, L_wrist);
            SquatCounter.R_elbow_angle = get3DAngle(R_shoulder, R_elbow, R_wrist);

            SquatCounter.L_shoulder_angle = get3DAngle(L_elbow, L_shoulder, L_hip);
            SquatCounter.R_shoulder_angle = get3DAngle(R_elbow, R_shoulder, R_hip);

            SquatCounter.L_knee_angle = get3DAngle(L_hip, L_knee, L_ankle);
            SquatCounter.R_knee_angle = get3DAngle(R_hip, R_knee, R_ankle);

            setAngleText(L_Elbow_Text, SquatCounter.L_elbow_angle.ToString());
            setAngleText(R_Elbow_Text, SquatCounter.R_elbow_angle.ToString());

            setAngleText(L_Shoulder_Text, SquatCounter.L_shoulder_angle.ToString());
            setAngleText(R_Shoulder_Text, SquatCounter.R_shoulder_angle.ToString());

            setAngleText(L_Knee_Text, SquatCounter.L_knee_angle.ToString());
            setAngleText(R_Knee_Text, SquatCounter.R_knee_angle.ToString());

            setAngleText(L_Elbow_Text_Box, SquatCounter.L_elbow_angle.ToString());
            setAngleText(R_Elbow_Text_Box, SquatCounter.R_elbow_angle.ToString());

            setAngleText(L_Shoulder_Text_Box, SquatCounter.L_shoulder_angle.ToString());
            setAngleText(R_Shoulder_Text_Box, SquatCounter.R_shoulder_angle.ToString());

            setPosText(L_Shoulder_Text , L_shoulder.X * 1200 - 600, (1 - L_shoulder.Y) * 540 - 270, -250);
            setPosText(R_Shoulder_Text , R_shoulder.X * 1200 - 600, (1 - R_shoulder.Y) * 540 - 270, -250);

            setPosText(L_Elbow_Text , L_elbow.X * 1200 - 600, (1 - L_elbow.Y) * 540 - 270, -250);
            setPosText(R_Elbow_Text , R_elbow.X * 1200 - 600, (1 - R_elbow.Y) * 540 - 270, -250);

            setPosText(L_Wrist_Text , L_wrist.X * 1200 - 600, (1 - L_wrist.Y) * 540 - 270, -250);
            setPosText(R_Wrist_Text , R_wrist.X * 1200 - 600, (1 - R_wrist.Y) * 540 - 270, -250);

            setPosText(L_Hip_Text , L_hip.X * 1200 - 600, (1 - L_hip.Y) * 540 - 270, -250);
            setPosText(R_Hip_Text , R_hip.X * 1200 - 600, (1 - R_hip.Y) * 540 - 270, -250);
            
            setPosText(L_Knee_Text , L_knee.X * 1200 - 600, (1 - L_knee.Y) * 540 - 270, -250);
            setPosText(R_Knee_Text , R_knee.X * 1200 - 600, (1 - R_knee.Y) * 540 - 270, -250);

            setPosText(L_Ankle_Text , L_ankle.X * 1200 - 600, (1 - L_ankle.Y) * 540 - 270, -250);
            setPosText(R_Ankle_Text , R_ankle.X * 1200 - 600, (1 - R_ankle.Y) * 540 - 270, -250);
            
            SquatCounter.counter();

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

    public void setAngleText(Text text_box, string newText){
        text_box.text = newText;
    }

    public void setPosText(Text text_box, double posX, double posY, double posZ){
        text_box.transform.position = new Vector3((float)posX, (float)posY, (float)posZ);
    }

}
