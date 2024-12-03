using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class klid : MonoBehaviour
{
    bool is_move = false;
    int taraf;
    public float min_x;
    public float max_x;
    public float min_z;
    public float max_z;
    private float time_wait = 1;
    // Start is called before the first frame update
    void Start()
    {
        print(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(time_wait>=0)
        {
            time_wait -= Time.deltaTime;
        }
        else
        {
            taraf = Random.Range(0, 4);
            if(can())
            {
                time_wait = 1;
                move();
            }
        }
    }
    private void move()
    {
        //225
        Vector3 pos;
        if(taraf==0)
        {
            transform.rotation = new Quaternion(0, -0, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 225);
            // up
        }
        if(taraf==1)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector3(transform.position.x+225, transform.position.y, transform.position.z);
            // rast
        }
        if(taraf==2)
        {
            transform.rotation = new Quaternion(0,0, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 225);
            //down
        }
        if(taraf==3)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.position = new Vector3(transform.position.x - 225, transform.position.y, transform.position.z);
            //left
        }
    }
    private bool can()
    {
        if (taraf == 0)
        {
            return transform.position.z + 225 <= max_z && transform.position.z + 225 >= min_z;
        }
        if (taraf == 1)
        {
            return transform.position.x + 225 <= max_x && transform.position.z + 225 >= min_x;
        }
        if (taraf == 2)
        {
            return transform.position.z - 225 <= max_z && transform.position.z - 225 >= min_z;
        }
        if (taraf == 3)
        {
            return transform.position.x - 225 <= max_x && transform.position.z - 225 >= min_x;
        }
        return false;
    }
}
