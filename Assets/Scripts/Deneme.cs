using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public GameObject BodyPrefab;
    public List<GameObject> BodyParts = new List<GameObject>();
    public List<Vector3> PositionHistory = new List<Vector3>();
    public int Gap = 10;
    public int index;
    public float BodySpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        AddBody();
        AddBody();
        AddBody();
        AddBody();
        AddBody();
        AddBody();
        AddBody();
        AddBody();
    }

    // Update is called once per frame
    void Update()
    {

        //Follow
        PositionHistory.Insert(0, transform.position);
        index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * Gap, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
        /*if (PositionHistory.Count > 1000)
        {
            PositionHistory.Clear();
        }*/
        //Follow
    }

    //Follow
    public void AddBody()
    {
        GameObject body = Instantiate(BodyPrefab, new Vector3(20f, 3.75f, -42f), Quaternion.identity);
        BodyParts.Add(body);
    }
    //Follow
}
