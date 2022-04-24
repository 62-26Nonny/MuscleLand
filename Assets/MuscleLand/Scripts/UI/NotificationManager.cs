using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject profileNoti;

    [SerializeField] GameObject exploreNoti;


    void Start()
    {

        if(Player.Level != Player.last_LV)
        {
            profileNoti.SetActive(true);
        } 
        else 
        {
            profileNoti.SetActive(false);
        }

        if(Player.total_reward > 0)
        {
            exploreNoti.SetActive(true);
        } 
        else 
        {
            exploreNoti.SetActive(false);
        }

    }
}
