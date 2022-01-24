using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newprogress : MonoBehaviour
{

    public List<GameObject> missionbox;
    void Start()
    {
        progresstext();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void progresstext()
    {
        float Svalue ;
        float Smaxvalue;

        for (int i = 0; i < missionbox.Count; i++) {

            Svalue = missionbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().value;
            Smaxvalue = missionbox[i].transform.Find("Progress Slider").gameObject.GetComponent<Slider>().maxValue;


            if (Svalue < Smaxvalue)
            {
                missionbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = Svalue.ToString() + "/" + Smaxvalue.ToString();
            }
            else 
            {
                missionbox[i].transform.Find("progress Text").gameObject.GetComponent<Text>().text = Smaxvalue.ToString() + "/" + Smaxvalue.ToString();
                missionbox[i].transform.Find("Button").gameObject.SetActive(true);
            }

        }
    }
}
