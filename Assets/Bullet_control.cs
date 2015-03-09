using UnityEngine;
using System.Collections;

public class Bullet_control : MonoBehaviour {

    public int LimitTime = 100;

    private int BufTime = 0;
	// Use this for initialization
	void Start () {
        BufTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (BufTime > LimitTime)
        {
            Destroy(gameObject);
        }
        else BufTime++;
	}
}
