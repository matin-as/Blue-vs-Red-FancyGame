using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyUI.Toast;

public class player : MonoBehaviour
{
    // movment
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 firstperson_view_Rotation = Vector3.zero;
    public CharacterController char_Contoroler;
    public float gravity = 10f;
    private float antiBumpfactor;
    private float speed ;
    private bool limitspeed = true;
    private float inputmodifyfactor;
    private float inputx;
    private float inputy;
    private float input_set_x;
    private float input_set_y;
    private bool is_grounded;
    private bool is_moving;
    private float record;
    private bool lose = false;
    private FixedJoystick Joystick;
    private Vector3 direction;
    //end movment
    [SerializeField] GameObject pl_g;
    [SerializeField] GameObject hart_ga;
    private AudioSource audioSource;
    int hart=0;
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("play convas").gameObject);
        Instantiate(Resources.Load<GameObject>("play convas"));
        Instantiate(Resources.Load<GameObject>("Back"), GameObject.Find("play convas").transform.transform);

        audioSource = GetComponent<AudioSource>();
        c_music();
        g=  Instantiate(pl_g);
        g.transform.position =new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-0.7f, gameObject.transform.position.z);
        speed = 495;
        char_Contoroler = GetComponent<CharacterController>();
        Joystick = GameObject.Find("play convas(Clone)").transform.GetChild(2).GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        m();
        movment();
    }
    public void FixedUpdate()
    {
        direction = Joystick.Horizontal * speed * transform.right + Joystick.Vertical * speed * transform.forward;
        direction = direction * Mathf.Min(1f, direction.magnitude);
    }
    private void movment()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Joystick.transform.position = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.W))
            {
                input_set_y = 1f;
            }
            else
            {
                input_set_y = -1f;
            }

        }
        else
        {
            input_set_y = 0f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                input_set_x = -1f;
            }
            else
            {
                input_set_x = 1f;
            }

        }
        else
        {
            input_set_x = 0f;
        }
        inputy = Mathf.Lerp(inputy, input_set_y, Time.deltaTime * 19f);
        inputx = Mathf.Lerp(inputx, input_set_x, Time.deltaTime * 19f);
        inputmodifyfactor = Mathf.Lerp(inputmodifyfactor,
            (input_set_x != 0 && input_set_y != 0 && limitspeed) ? 0.75f : 1.0f,
            Time.deltaTime * 19f);

        firstperson_view_Rotation = Vector3.Lerp(firstperson_view_Rotation,
           Vector3.zero, Time.deltaTime * 5f);


            moveDirection = new Vector3(inputx * inputmodifyfactor, -antiBumpfactor,
                inputy * inputmodifyfactor);
            moveDirection = transform.TransformDirection(moveDirection) * speed;
        moveDirection.y -= gravity * Time.deltaTime;
        is_grounded = (char_Contoroler.Move(direction * Time.deltaTime) & CollisionFlags.Below) != 0;
        //char_Contoroler.Move(moveDirection * Time.deltaTime);
        is_moving = char_Contoroler.velocity.magnitude > 0.15f;
    }
    private void OnTriggerEnter(Collider other)
    {
        teleport(other.name);
        destroy_key(other.name);
        if (other.tag=="hart")
        {
            GameObject a = GameObject.Find("content");
            Instantiate(hart_ga, a.transform.transform);
            hart++;
            Destroy(other.gameObject);
        }
        if (other.tag == "enemy")
        {
            if(hart>=1)
            {
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(g.transform.position.x, g.transform.position.y + 0.7f, g.transform.position.z);
                gameObject.SetActive(true);
                Destroy(GameObject.Find("content").transform.GetChild(0).gameObject);
                hart--;
            }
            else
            {
                lose = true;
                PlayerPrefs.SetInt("losed_in_level" + PlayerPrefs.GetInt("selected_level").ToString(), PlayerPrefs.GetInt("losed_in_level" + PlayerPrefs.GetInt("selected_level")) + 1);
                PlayerPrefs.SetInt("all_die", PlayerPrefs.GetInt("all_die") + 1);
                SceneManager.LoadScene(PlayerPrefs.GetInt("selected_level"));
                record = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                lose = false;
            }
        }
        if (other.name== "end ")
        {
            try
            {
                print(GameObject.FindGameObjectWithTag("coin").name);
                _ShowAndroidToastMessage("You haven't won all the coins!");
            }
            catch
            {
                if(PlayerPrefs.GetFloat("record_in_level" + PlayerPrefs.GetInt("selected_level"))!=0)
                {
                    if (PlayerPrefs.GetFloat("record_in_level" + PlayerPrefs.GetInt("selected_level")) > record)
                    {
                        PlayerPrefs.SetFloat("record_in_level" + PlayerPrefs.GetInt("selected_level").ToString(), record);
                        PlayerPrefs.SetFloat("time", record);
                        record = 0;
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("time", record);
                        record = 0;
                    }
                }
                else
                {
                    PlayerPrefs.SetFloat("record_in_level" + PlayerPrefs.GetInt("selected_level").ToString(), record);
                    PlayerPrefs.SetFloat("time", record);
                    record = 0;
                }
                Destroy(gameObject);
                PlayerPrefs.SetString("show_c", "yes");
                print(record);
            }
            // do some thing
        }
    }
    private void m()
    {
            record += Time.deltaTime;
    }
    private void c_music()
    {
        if(PlayerPrefs.GetString("music") =="on")
        {
            Destroy(audioSource);

        }
    }
    private void teleport(string name_g)
    {
        List<char> ls = new List<char>();
       foreach(char w in name_g)
        {
            ls.Add(w);
            if (string.Join("", ls) == "teleport")
            {
                try
                {
                    GameObject g1 = GameObject.Find(name_g);
                    int i;
                    int.TryParse(string.Join("", name_g[name_g.Length - 1]), out i);
                    GameObject g2 = GameObject.Find("teleport" + (i + 1));
                    print(g2.transform.position);
                    print("code 1 Run ! ");
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(g2.transform.position.x+100, gameObject.transform.position.y, g2.transform.position.z);
                    gameObject.SetActive(true);
                }
                catch
                {
                    GameObject g1= GameObject.Find(name_g);
                    int i;
                    int.TryParse(string.Join("", name_g[name_g.Length - 1]), out i);
                    GameObject g2 = GameObject.Find("teleport" + (i - 1));
                    print(g2.transform.position);
                    print("code 2 Run ! ");
                    gameObject.SetActive(false);
                    gameObject.transform.position = new Vector3(g2.transform.position.x+100, gameObject.transform.position.y, g2.transform.position.z);
                    gameObject.SetActive(true);
                }
            }
        }
    }
    private void destroy_key(string name_g)
    {
        List<char> ls = new List<char>();
        foreach (char w in name_g)
        {
            ls.Add(w);
            if (string.Join("", ls) == "key")
            {
                int i;
                int.TryParse(string.Join("", name_g[name_g.Length - 1]), out i);
                print("Okey");
                Destroy(GameObject.Find(name_g));
                Destroy(GameObject.Find("keydivar"+i.ToString()));
            }
        }
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

