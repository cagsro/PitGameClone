using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMoving : MonoBehaviour
{
    public static ParentMoving instance;
    public float speed = 9f;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    void Start()
    {
        speed = 9f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
}
