using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject EnemyModel;
    public GameObject EnemyModel_Boss;
    public GameObject Point;
    public int Interval = 100;//発生間隔
    public int Parallel = 5;//同時発生数
    public int IntervalBoss = 200;
    public AudioClip StartVoice;
    public int LimitChild = 20;

    private int BufInterval = 0;
    private int BufIntervalBoss = 0;
    private int Flag = 0;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Flag != 0)
        {
            if (transform.childCount < LimitChild)
            {
                if (BufInterval > Interval)
                {
                    CreateModel();
                    BufInterval = 0;
                    if (Interval > 10) Interval -= 10;
                }
                else BufInterval++;
            }
            Debug.Log(transform.childCount);

            if (BufIntervalBoss > IntervalBoss)
            {
                Vector3 v = Point.transform.position;
                v.x += Random.Range(-5.0f, 5.0f);
                v.y = Random.Range(0.0f, 5.0f);
                Instantiate(EnemyModel_Boss, v, Point.transform.rotation);
                BufIntervalBoss = 0;
            }
            else BufIntervalBoss++;
        }
        else
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                Flag = 1;
                audio.clip = StartVoice;
                audio.Play();
                CreateModel();
            }
        }
	}

    private void CreateModel()
    {
        int max=Random.Range(1, Parallel);
        for (int i = 0; i < max; i++)
        {
            Vector3 v = Point.transform.position;
            v.x += Random.Range(-5.0f, 5.0f);
            GameObject g = (GameObject)Instantiate(EnemyModel, v, Point.transform.rotation);
            g.transform.parent = transform;
        }
    }
}
