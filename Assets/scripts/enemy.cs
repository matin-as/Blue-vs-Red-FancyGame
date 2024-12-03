using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    private NavMeshAgent enemy_;
    GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        print("://");
        a = GameObject.FindGameObjectsWithTag("a")[random.Range(0, GameObject.FindGameObjectsWithTag("a").Length)];
        enemy_ = GetComponent<NavMeshAgent>();
        enemy_.speed = 500.5f;
    }

    // Update is called once per frame
    void Update()
    {
            if (transform.position.x == a.transform.position.x && transform.position.z == a.transform.position.z)
            {
                print(GameObject.FindGameObjectsWithTag("a")[random.Range(0, GameObject.FindGameObjectsWithTag("a").Length)].name);
                a = GameObject.FindGameObjectsWithTag("a")[random.Range(0, GameObject.FindGameObjectsWithTag("a").Length)];
            }
            enemy_.SetDestination(a.transform.position);
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
