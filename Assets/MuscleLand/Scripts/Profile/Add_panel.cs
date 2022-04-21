using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Add_panel : MonoBehaviour
{
    public GameObject list;

    public GameObject Prefab;

    //public GameObject test_text;

    // public void add_component()
    // {
    //     GameObject box = Instantiate(Prefab);

    //     box.transform.SetParent(list.transform, false);

    //     Transform boxtran = box.transform;

    //     Transform img = boxtran.Find("Image");
    //     img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("games");

    //     Transform txt = boxtran.Find("Text");
    //     txt.transform.GetComponent<Text>().text = "hello";

    //     Button test = boxtran.Find("Button").GetComponent<Button>();
    //     test.onClick.AddListener(() => Showpopup("Test"));
    // }
    public void add_component_reward(string name, int amount, string imgLocation)
    {
        GameObject box = Instantiate(Prefab);

        box.transform.SetParent(list.transform, false);

        box.SetActive(true);


        Transform boxtran = box.transform;

        Transform img = boxtran.Find("Image");
        img.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgLocation);

        Transform txt = boxtran.Find("Text");
        txt.transform.GetComponent<Text>().text = amount.ToString() + " " + name;
    }

    public void clear_component()
    {
        foreach (Transform child in list.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    // public void Showpopup(string name)
    // {
    //     Text txt = test_text.transform.GetComponent<Text>();
    //     txt.text = name;
    // }
}
