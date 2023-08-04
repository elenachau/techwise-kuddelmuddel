using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedObject : MonoBehaviour
{
    public GameObject weedObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Weed guy script");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown() {
        Debug.Log("Weed guy clicked!");
        GameObject newWeed = Instantiate(weedObject, transform.position + new Vector3(1,1,0), Quaternion.identity);
        newWeed.transform.parent = UnityEngine.GameObject.Find("Above Ground").transform;
    }
}
