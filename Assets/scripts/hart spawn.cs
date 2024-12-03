using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

public class hartspawn : MonoBehaviour
{
    [SerializeField] GameObject g;
    private float y = 359.9999f;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    private float timer = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer>=0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            print("A");
            g =  Instantiate(g, new Vector3(random.Range(minX, maxX), y, random.Range(minZ, maxZ)),Quaternion.identity);
            g.SetActive(false);
            g.transform.Rotate(new Vector3(0,90,90));
            g.SetActive(true);
            timer = 7;
        }
    }
}
