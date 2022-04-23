using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Imageclick : MonoBehaviour
{   
    public GameObject imagebox;
    public GameObject imageEdit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.name == imageEdit.name){
            this.GetComponent<Image>().color = new Color32(111,111,111,100);
            this.GetComponent<Button>().enabled = false;
        }else{
            this.GetComponent<Image>().color = new Color32(255,255,255,255);
            this.GetComponent<Button>().enabled = true;
        }
    }

    public void ChangeProfileImage(){
        SFX.Instance.playClickSound();
        imageEdit.name = this.name;
        var NewImage  =  Resources.Load<Sprite>("Profileimage/"+imageEdit.name);
        imageEdit.GetComponent<Image>().sprite = NewImage;
        imagebox.SetActive(false);
    }
}
