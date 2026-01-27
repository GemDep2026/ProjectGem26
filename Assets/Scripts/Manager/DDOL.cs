using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    [HideInInspector]
    public string objectID;
    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }

    private void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DDOL>().Length; i++) 
        { 
            if (Object.FindObjectsOfType<DDOL>()[i] != this)
            {
                if (Object.FindObjectsOfType<DDOL>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }    
        }
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
