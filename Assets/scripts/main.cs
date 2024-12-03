using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System;
using System.Collections.Specialized;
using System.Text;
using RTLTMPro;
using EasyUI.Toast;

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param);
        });
    }
}


public class main : MonoBehaviour
{
    [SerializeField] GameObject vv;
    [SerializeField] GameObject btn_ne;
    [SerializeField] GameObject g_more;
    [SerializeField] GameObject t;
    [SerializeField] GameObject t1;
    [SerializeField] ScrollRect rect;
    [SerializeField] RectTransform a;
    [SerializeField] RectTransform b;
    public GameObject f;
    public GameObject r;
    public GameObject re;
    public GameObject lo;
    [SerializeField] Sprite ok_img;
    [SerializeField] Sprite no_img;
    [SerializeField] Sprite no_btn_img;
    [SerializeField] Sprite music_s;
    [SerializeField] Sprite no_music;
    [SerializeField] GameObject music_g;
   public struct User
    {
        public string name;
        public string Loses;
        public string Record;
    }
    List<User> Data_loses = new List<User>();
    List<User> Data_Record = new List<User>();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("music")==null)
        {
            PlayerPrefs.SetString("music", "on");
        }
        check_music();
        if (gameObject.name== "content")
        {
            GameObject faza = transform.GetChild(0).gameObject;
            GameObject g;
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if(i!=0)
                {
                    g = Instantiate(faza, transform);
                    if(i<=PlayerPrefs.GetInt("level"))
                    {
                        g.transform.GetChild(1).GetComponent<Image>().sprite = ok_img;
                        g.transform.GetChild(3).GetComponent<RTLTextMeshPro>().text = "Time:" + Mathf.Round(PlayerPrefs.GetFloat("record_in_level" + i.ToString())).ToString();
                        g.transform.GetChild(4).GetComponent<RTLTextMeshPro>().text = "Loss:" + Mathf.Round(PlayerPrefs.GetInt("losed_in_level" + i.ToString())).ToString();

                    }
                    else
                    {
                        g.transform.GetChild(0).GetComponent<Image>().sprite = no_btn_img;
                        g.transform.GetChild(1).GetComponent<Image>().sprite = no_img;
                    }
                    g.transform.GetChild(2).GetComponent<RTLTextMeshPro>().text = "Level:" + i.ToString();
                    g.transform.GetChild(0).GetComponent<Button>().AddEventListener(i, ItemClicked);
                }

                //g.transform.GetChild(0).GetComponent<Image>().sprite = DataItems[i].icon;

            }
            Destroy(faza);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void ItemClicked(int itemIndex)
    {
        if(itemIndex<=PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("selected_level", itemIndex);
            SceneManager.LoadScene(itemIndex);
        }
        else
        {
            // cant go to this game
        }
    }
        public void play()
        {
        if(PlayerPrefs.GetInt("level")==0|| PlayerPrefs.GetInt("level") == 1)
        {
            PlayerPrefs.SetInt("level",1);
            PlayerPrefs.SetInt("selected_level",1);
            SceneManager.LoadScene(1);
        }
        else
        {
            f.SetActive(true);
            btn_ne.SetActive(true);
        }
    }
    public void gomain()
    {
        if (PlayerPrefs.GetInt("selected_level") == PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        }
        SceneManager.LoadScene(0);
    }
    public void next()
    {
        if(PlayerPrefs.GetInt("selected_level")==PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("selected_level", PlayerPrefs.GetInt("selected_level") + 1);
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("level"));
        }
        else
        {
            PlayerPrefs.SetInt("selected_level",PlayerPrefs.GetInt("selected_level") +1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("selected_level"));
        }
    }
    public void again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Rank()
    {
        if(PlayerPrefs.GetInt("internet")==1)
        {
            if (r.activeInHierarchy)
            {
                r.SetActive(false);
                t1.SetActive(false);
                t.SetActive(false);
                vv.SetActive(false);
            }
            else
            {
                r.SetActive(true);
                t1.SetActive(true);
                t.SetActive(true);
                vv.SetActive(true);
            }
        }
        else
        {
            _ShowAndroidToastMessage("اتصال اینترنت خود را بررسی نمایید");
        }
    }
    public void s_r()
    {
        re.SetActive(true);
        lo.SetActive(false);
        rect.content = b;
    }
    public void s_l()
    {
        re.SetActive(false);
        lo.SetActive(true);
        rect.content = a;
    }

    public void music()
    {
        if(PlayerPrefs.GetString("music") =="on")
        {
            PlayerPrefs.SetString("music", "off");
        }
        else
        {
            PlayerPrefs.SetString("music", "on");
        }
        check_music();
    }
    private void check_music()
    {
        if(music_g.GetComponent<Image>()==null)
        {
            return;
        }
        if (PlayerPrefs.GetString("music") == "on")
        {
            music_g.GetComponent<Image>().sprite = no_music;
        }
        else
        {
            music_g.GetComponent<Image>().sprite = music_s;
        }

    }
    public void back()
    {
        SceneManager.LoadScene(0);
    }
    private void _ShowAndroidToastMessage(string message)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
        else
        {
            Toast.Show(message, 3f, ToastColor.Red);
            //print(message);
        }
    }
    public void more()
    {
        if(g_more.activeInHierarchy)
        {
            g_more.SetActive(false);
        }
        else
        {
            g_more.SetActive(true);
        }
    }
    public void On_rating_btn()
    {
        Application.OpenURL("myket://comment?id=com.mis.blueman");
    }
    public void On_applicationpage_btn()
    {
        Application.OpenURL("myket://details?id=com.mis.blueman");
    }
    public void On_List_btn()
    {
        Application.OpenURL("myket://developer/com.mis.blueman");

    }
    public void skip_level()
    {
        GameObject t = GameObject.Find("ads manager");
        if(t.GetComponent<ads>().is_loaded_ad())
        {
            t.GetComponent<ads>().show_ad();
            // show the Add 
        }
        else
        {
            _ShowAndroidToastMessage("منتظر لود شدن تبلیغات باشید");
        }
    }
}
