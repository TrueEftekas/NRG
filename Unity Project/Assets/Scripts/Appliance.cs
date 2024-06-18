using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance : MonoBehaviour
{
    //where we store info regarding each appliance
    public bool good = true;
    public bool isActuallyGood = true;
    public List<string> whys;
    public List<bool> problem;
    public List<bool> realAnswers;
    public string location;
    public int floor;
    public bool isOn;
    public string data;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
