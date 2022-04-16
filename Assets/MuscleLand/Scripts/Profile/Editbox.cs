using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editbox : MonoBehaviour
{   
    
    public Text fillText;
    public Text NewUserName;
    public GameObject GameEditBox;
     public GameObject ImageEdit;
    public GameObject ImageProfileEditBox;
    // Start is called before the first frame update
    void Start()
    {   
        var NewImage  =  Resources.Load<Sprite>("Profileimage/"+Player.userpic);
        ImageEdit.GetComponent<Image>().sprite = NewImage;
        Debug.Log("Profileimage/"+Player.userpic);
        fillText.text = Player.username;
        ImageEdit.name = Player.userpic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CancelEdit()
    {
        GameEditBox.SetActive(false);
    }
    public void EditProfile()
    {
        GameEditBox.SetActive(true);
    }

    public void SaveProfile(){
        if (NewUserName.text == "")
        {
            Player.username = fillText.text;
        }else{
            Player.username = NewUserName.text;
        }
        Player.userpic = ImageEdit.name;
        WWWForm form = new WWWForm();
        form.AddField("username", Player.username);
        form.AddField("profilepic", Player.userpic);
        StartCoroutine(WebRequest.Instance.PostRequest("/user/" + Player.userID.ToString(), form));
        GameEditBox.SetActive(false);
    }

    public void ImageProfileChange()
    {
        ImageProfileEditBox.SetActive(true);
    }
}
