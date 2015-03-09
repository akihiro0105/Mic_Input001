using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public GameObject Text;
    public GameObject[] Food;
    public AudioClip GoalVoice;

    private int HitPoint = 5;
    private float T=0;
    private Text stringtext;
    private AudioSource audio;
    private bool Flag = false;
	// Use this for initialization
	void Start () {
        HitPoint = Food.Length;
        stringtext = Text.GetComponent<Text>();
        stringtext.text = "らいふ " + HitPoint.ToString();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (HitPoint<=0)
        {
            Debug.Log(T);
            stringtext.text = "たいむ "+T.ToString("f1");
            if (Flag == false)
            {
                audio.clip = GoalVoice;
                audio.Play();
                Flag = true;
            }
        }
        else
        {
            T = Time.time;
            stringtext.text = "らいふ " + HitPoint.ToString();
        }
        if (Input.GetAxis("Fire2") != 0)
        {
            Application.LoadLevel("Mic_Input001");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            HitPoint--;
            if (HitPoint>=0) Destroy(Food[HitPoint]);
        }
    }
}
