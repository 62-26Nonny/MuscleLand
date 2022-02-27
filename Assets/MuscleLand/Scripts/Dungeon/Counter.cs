using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] public Text stage_text;
    [SerializeField] public Text count_text;
    [SerializeField] public Text hand_status;
    [SerializeField] public Text leg_status;
    [SerializeField] public Text stand_status;
    public static string stage = "None";
    public static int count = 0;
    public static int L_elbow_angle;
    public static int R_elbow_angle;
    public static int L_shoulder_angle;
    public static int R_shoulder_angle;
    public static int L_hip_angle;
    public static int R_hip_angle;
    public static int L_knee_angle;
    public static int R_knee_angle;

    public static bool hand_gesture() {
        switch (DungeonValues.Dungeon_displayname){
            case "Squat":
                if (L_elbow_angle >= 150 & R_elbow_angle >= 150 & L_shoulder_angle <= 130 & R_shoulder_angle <= 130 
                & L_shoulder_angle >= 80 & R_shoulder_angle >= 80) {
                    return true;
                }
                else{
                    return false;
                }
            case "Jumping Jack":
                if (L_elbow_angle >= 130 & R_elbow_angle >= 130 & L_shoulder_angle <= 175 & R_shoulder_angle <= 175 
                & L_shoulder_angle >= 145 & R_shoulder_angle >= 145) {
                    return true;
                }
                else{
                    return false;
                }
            case "Rising Knee":
                if (L_elbow_angle >= 150 & R_elbow_angle >= 150 & L_shoulder_angle <= 130 & R_shoulder_angle <= 130 
                & L_shoulder_angle >= 80 & R_shoulder_angle >= 80) {
                    return true;
                }
                else{
                    return false;
                }
            default:
                return false;
        }
    }

    public static bool leg_gesture() {
        switch (DungeonValues.Dungeon_displayname){
            case "Squat":
                if (L_knee_angle <= 60 & R_knee_angle <= 60) {
                    return true;
                }
                else {
                    return false;
                }
            case "Jumping Jack":
                if (L_knee_angle >= 165 & R_knee_angle >= 165 & L_hip_angle <= 170 & R_hip_angle <= 170 & L_hip_angle >= 160 & R_hip_angle >= 160) {
                    return true;
                }
                else {
                    return false;
                }
            case "Rising Knee":
                if (L_knee_angle <= 60 & R_knee_angle <= 60) {
                    return true;
                }
                else {
                    return false;
                }
            default:
                return false;
        }
    }

    public static bool isStand() {
        switch (DungeonValues.Dungeon_displayname){
            case "Squat":
                if (hand_gesture() & L_knee_angle >= 130 & R_knee_angle >= 130) {
                    return true;
                }
                else {
                    return false;
                }
            case "Jumping Jack":
                if (L_shoulder_angle <= 35 & R_shoulder_angle <= 35 & L_elbow_angle >= 100 & R_elbow_angle >= 100 & 
                L_knee_angle >= 110 & R_knee_angle >= 110 & L_hip_angle >= 135 & R_hip_angle >= 135) {
                    return true;
                }
                else {
                    return false;
                }
            case "Rising Knee":
                if (hand_gesture() & L_knee_angle >= 130 & R_knee_angle >= 130) {
                    return true;
                }
                else {
                    return false;
                }
            default:
                return false;
        }
    }

    public static bool isCorrectGesture() {
        if (hand_gesture() & leg_gesture()) {
            return true;
        }
        else {
            return false;
        }
    }

    public static void counter() {
        if (isCorrectGesture()) {
            stage = "Down";
        }
        
        if (isStand() & stage == "Down") {
            stage = "Up";
            count += 1;
            Destroyer.Destruction();            
        }
    }

    private void Update() {
        stage_text.text = stage;
        count_text.text = "Reps: " + count.ToString();
        hand_status.text = "Hand: " + hand_gesture().ToString();
        leg_status.text = "Leg: " + leg_gesture().ToString();
        stand_status.text = "Stand: " + isStand().ToString();
    }
}
