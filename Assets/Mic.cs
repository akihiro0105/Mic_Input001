using UnityEngine;
using System.Collections;

public class Mic : MonoBehaviour {
    public GameObject ShotPoint;
    public GameObject Bullet=null;
    public float MicLevel = 10.0f;
    public int Interval = 10;
    public float Power = 30.0f;

    private float[] data;
    private int IntervalCount = 0;

	// Use this for initialization
	void Start () {
        data = new float[256];
        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true;
        audio.mute = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audio.Play();
	
	}
	
	// Update is called once per frame
	void Update () {
        float a = 0.0f;
        audio.GetOutputData(data, 0);
        foreach (float s in data) a += Mathf.Abs(s);
        float v = (a / 256.0f) * 100;
        print(v.ToString());
        if (v > MicLevel)
        {
            if (IntervalCount >= Interval)
            {
                if (Bullet)
                {
                    GameObject bullet = (GameObject)Instantiate(Bullet, ShotPoint.transform.position, Quaternion.identity);
                    bullet.rigidbody.velocity = ShotPoint.transform.forward * Power;
                }
                IntervalCount = 0;
            }
            else
            {
                IntervalCount++;
            }
        }
        else
        {
            IntervalCount = 0;
        }
	}
}
