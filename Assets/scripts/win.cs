using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RTLTMPro;
public class win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name != "play convas(Clone)")
        {
            Destroy(transform.GetChild(0).gameObject);
             GameObject g = Instantiate(Resources.Load<GameObject>("convas_win"),transform.transform);
            g.name = "convas_win";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name== "play convas(Clone)")
        {
            print(PlayerPrefs.GetInt("all_die"));
            gameObject.transform.GetChild(1).gameObject.GetComponent<RTLTextMeshPro>().text = "Losses :" + PlayerPrefs.GetInt("all_die").ToString();
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).GetComponent<RTLTextMeshPro>().text = "Losses :" + Mathf.Round(PlayerPrefs.GetInt("losed_in_level" + PlayerPrefs.GetInt("selected_level").ToString())).ToString();
            gameObject.transform.GetChild(0).transform.GetChild(6).transform.GetChild(0).GetComponent<RTLTextMeshPro>().text = "Record : " + Mathf.Round(PlayerPrefs.GetFloat("record_in_level" + PlayerPrefs.GetInt("selected_level").ToString())).ToString();
            gameObject.transform.GetChild(0).transform.GetChild(7).transform.GetChild(0).GetComponent<RTLTextMeshPro>().text = "Time : " + Mathf.Round(PlayerPrefs.GetFloat("time")).ToString();
            if (PlayerPrefs.GetString("show_c") == "yes")
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                PlayerPrefs.SetString("show_c", "no");
            }
        }
    }
}
