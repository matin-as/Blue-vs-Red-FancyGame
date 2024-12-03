using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Threading;
using RTLTMPro;
using EasyUI.Toast;
using Newtonsoft.Json;
public class server : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI o;
    [SerializeField] GameObject y;
    [SerializeField] GameObject y2;
    [SerializeField] Text text_user;
    [SerializeField] TMP_InputField n;
    [SerializeField] TMP_InputField p;
    [SerializeField] GameObject set_name;
    [SerializeField] GameObject faza;
    [SerializeField] GameObject r;
    [SerializeField] GameObject r2;
    private string url_l = "https://example.com/";
    private string url_t = "https://example.com/";
    private string get_rank = "https://example.com/";
    private string get_rank2 = "https://example.com/";
    public struct user
    {
        public string name;
        public string leve;
        public string lose;
        public string time;
        public string ranking;
    }
    public List<user> users = new List<user>();
    public List<user> users_ = new List<user>();
    public List<user> g2 = new List<user>();
    public List<user> g = new List<user>();
    // Start is called before the first frame update
    void Start()
    {
        text_user.text = PlayerPrefs.GetString("username");
        //if (PlayerPrefs.GetString("username") == "")
        //{
        //    set_name.SetActive(true);
        //}
        //else
        //{
        //    update_Ac();
        //    getrank(users, url_t, r,true);
        //    getrank(users_, url_l, r2,true);
        //    getrank(g, get_rank, y, false);
        //    getrank(g2, get_rank2, y2, false);
        //}

        //getrank(users,url_t);
        if (o.text != "")
        {
            PlayerPrefs.SetInt("internet", 1);
        }
        else
        {
            PlayerPrefs.SetInt("internet", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void sing_up()
    {
        // sing up
        if(n.text!=""&&p.text!=""&&n.text.Length>0&&p.text.Length>0)
        {
            try
            {
                string url_sing_up = "https://example.com/";
                WebClient webClient = new WebClient();
                NameValueCollection form = new NameValueCollection();
                form["name"] = n.text;
                form["password"] = p.text;
                form["time"] = Mathf.Round(getTime()).ToString(); ;
                form["lose"] = PlayerPrefs.GetString("all_die").ToString();
                form["level"] = PlayerPrefs.GetInt("level").ToString();
                byte[] b_res = webClient.UploadValues(url_sing_up, "POST", form);
                string res = Encoding.UTF8.GetString(b_res);
                webClient.Dispose();
                if(res=="0")
                {
                    set_name.SetActive(false);
                    PlayerPrefs.SetString("username", n.text);
                    PlayerPrefs.SetString("pass", p.text);
                    text_user.text = PlayerPrefs.GetString("username");
                    getrank(users, url_t,r,true);
                    getrank(users_, url_l, r2,true);
                    getrank(g, get_rank, y, false);
                    getrank(g2, get_rank2, y2, false);
                }
                else
                {
                    _ShowAndroidToastMessage("This username already exists.");
                }
            }
            catch
            {
                _ShowAndroidToastMessage("Check the internet connection.");
            }
        }
        else
        {
            _ShowAndroidToastMessage("Fill in all fields.");
        }

    }
    void update_Ac()
    {
        string url_sing_up = "https://example.com/";
        WebClient webClient = new WebClient();
        NameValueCollection form = new NameValueCollection();
        form["name"] = PlayerPrefs.GetString("username");
        form["password"] = PlayerPrefs.GetString("pass");
        form["time"] = Mathf.Round(getTime()).ToString();
        form["lose"] = PlayerPrefs.GetString("all_die").ToString()+1;
        form["level"] = PlayerPrefs.GetInt("level").ToString();
        try
        {
            byte[] b_res = webClient.UploadValues(url_sing_up, "POST", form);
            string res = Encoding.UTF8.GetString(b_res);
            webClient.Dispose();
            PlayerPrefs.SetInt("internet", 1);
        }
        catch
        {
            PlayerPrefs.SetInt("internet", 8);
        }
    }
    float getTime()
    {
        float time = 0f;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
        
            time =time + PlayerPrefs.GetFloat("record_in_level"+i);
        }
        return time;
    }
    private void getrank(List<user> Data_iteam, string url,GameObject parent,bool a)
    {
            Data_iteam.Clear();
            WebClient webClient = new WebClient();
            NameValueCollection form = new NameValueCollection();
            form["name"] = PlayerPrefs.GetString("username");
            byte[] b_res = webClient.UploadValues(url, "POST", form);
            string res = Encoding.UTF8.GetString(b_res);
            print(res);
            webClient.Dispose();
            JsonConvert.PopulateObject(res, Data_iteam);
        if (a)
            {
                for (int i = 0; i < 10; i++)
                {
                    GameObject g = Instantiate(faza, parent.transform);//faza// r
                    g.transform.GetChild(0).GetComponent<RTLTextMeshPro>().text = "Name : " + Data_iteam[i].name;
                    g.transform.GetChild(1).GetComponent<RTLTextMeshPro>().text = "Losses:" + Data_iteam[i].lose;
                    g.transform.GetChild(2).GetComponent<RTLTextMeshPro>().text = "Time : " + Data_iteam[i].time;
                    g.transform.GetChild(3).GetComponent<RTLTextMeshPro>().text = "Level : " + Data_iteam[i].leve;
                    g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = Data_iteam[i].ranking.ToString();
                }
            }
            else
            {
                print(url);
                print(Data_iteam[0].ranking);
                parent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Data_iteam[0].ranking;
            }
    }
    void destroy_list(GameObject g)
    {
        for (int i = 0; i < g.transform.childCount; i++)
        {
            GameObject d = g.transform.GetChild(i).gameObject;
            Destroy(d);
        }
    }
    public void refresh()
    {
        destroy_list(r);
        destroy_list(r2);
        update_Ac();
        getrank(users, url_t, r,true);
        getrank(users_, url_l, r2,true);
        getrank(g, get_rank, y, false);
        getrank(g2, get_rank2, y2, false);
        _ShowAndroidToastMessage("refreshed !");

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
        }
    }
}
