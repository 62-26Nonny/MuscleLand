using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SquatCounter : MonoBehaviour
{

    [SerializeField] public Text stage_text;
    [SerializeField] public Text count_text;
    [SerializeField] public Text hand_status;
    [SerializeField] public Text leg_status;
    public static string stage = "None";
    public static int count = 0;
    public static int L_elbow_angle;
    public static int R_elbow_angle;
    public static int L_shoulder_angle;
    public static int R_shoulder_angle;
    public static int L_knee_angle;
    public static int R_knee_angle;

    public static bool hand_gesture() {
        if (L_elbow_angle >= 150 & R_elbow_angle >= 150 & L_shoulder_angle <= 130 & R_shoulder_angle <= 130 
            & L_shoulder_angle >= 100 & R_shoulder_angle >= 100) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool leg_gesture() {
        if (L_knee_angle <= 60 & R_knee_angle <= 60) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool isStand() {
        if (hand_gesture() & L_knee_angle >= 150 & R_knee_angle >= 150) {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool isSquat() {
        if (hand_gesture() & leg_gesture()) {
            return true;
        }
        else {
            return false;
        }
    }

    public static void counter() {
        if (isSquat()) {
            stage = "Down";
        }
        
        if (isStand() & stage == "Down") {
            stage = "Up";
            count += 1;
        }
    }

    private void Update() {
        stage_text.text = stage;
        count_text.text = count.ToString();
        hand_status.text = hand_gesture().ToString();
        leg_status.text = leg_gesture().ToString();
    }
}
