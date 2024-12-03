using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tir : MonoBehaviour
{
    float i = 2;
    [SerializeField] GameObject tira;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GameObject.Find("player").transform);
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.name== "jenerate")
        {
            if(i>0)
            {
                i -= Time.deltaTime;
            }
            else
            {
                
                GameObject g = Instantiate(tira);
                g.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                print(g.name);
                i = 2;
            }
        }
        else
        {
            Vector3 f = transform.forward;
            f.y = 0f;
            f.Normalize();
            transform.position += f * 750 * Time.deltaTime;
        }
    }
}
