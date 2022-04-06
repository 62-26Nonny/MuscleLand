using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class AvatarManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] Avatars;
    List<string> Equipped_list = new List<string>();
    List<string> Appearance_list = new List<string>();

    private void Start(){
        StartCoroutine(WebRequest.Instance.GetRequest("/wearitem/" + Player.userID, (json) => 
        {
            WearItemSerializer[] res = JsonHelper.getJsonArray<WearItemSerializer>(json);
            foreach (var item in res)
            {
                Equipped_list.Add(item.itemID.ToString());
            }

            //Debug.Log("Equip List = " + Equipped_list[0]);

            if(int.Parse(Equipped_list[0]) == 0){
                //Debug.Log("Didn't Equip any");
                Avatars[0].SetActive(true);
                if(SceneManager.GetActiveScene().name == "Inventory"){
                    Avatars[0].GetComponent<Animator>().Play("Look Around");
                }
                return;                
            }

            using (var conection = new SqliteConnection(Database.Instance.dbClient))
            {
                conection.Open();
                using (var command = conection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM item ORDER BY itemID ;";
                    using (var reader = command.ExecuteReader())
                    {
                        foreach (var item in reader)
                        {
                            if(Equipped_list.Contains(reader["itemID"].ToString()))
                            {
                                // Appearance_list.Add(reader["appearance"].ToString());
                                //Debug.Log("Equip Avatar = " + Equipped_list[0]);
                                Avatars[int.Parse(Equipped_list[0])].SetActive(true);
                                if(SceneManager.GetActiveScene().name == "Inventory"){
                                    Avatars[int.Parse(Equipped_list[0])].GetComponent<Animator>().Play("Look Around");
                                }
                            } 
                            else {
                                //Avatars[int.Parse(reader["itemID"].ToString()) - 1].SetActive(false);
                                Avatars[0].SetActive(false);
                            }
                        }
                        reader.Close();
                    }
                }
                conection.Close();
            }

            // if(Appearance_list.Count > 0)
            // {
            //     GameObject.Find("Character").GetComponent<Image>().sprite = Resources.Load<Sprite>(Appearance_list[0]);
            // }
        }));
    }
    
}
