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

    }

    public void ChangeProfileImage(){
        imageEdit.name = this.name;
        var NewImage  =  Resources.Load<Sprite>("Profileimage/"+imageEdit.name);
        imageEdit.GetComponent<Image>().sprite = NewImage;
        imagebox.SetActive(false);
    }
}
