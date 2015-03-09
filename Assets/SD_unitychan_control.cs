using UnityEngine;
using System.Collections;

public class SD_unitychan_control : MonoBehaviour {

    public int HitPoint = 10;
    public float Speed = 0.01f;
    public GameObject CreateEffect;
    public GameObject DeleteEffect;
    public AudioClip DamageVoice;

    private Animator Anime;
    private int Flag = 0;
    private AudioSource audio;
	// Use this for initialization
	void Start () {
        Anime = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        Instantiate(CreateEffect, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
        switch (Flag)
        {
            case 0:
                if (UpdateInit())
                {
                    Flag = 1;
                }
                 break;
            case 1:
                 if (UpdateRun())
                 {
                     Flag = 2;
                 }
                break;
            case 2:
                if (UpdateEnd())
                {
                    Flag = 3;
                }
                break;
            default:
                Instantiate(DeleteEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
        }
	}

    //出現時のモーション
    private bool UpdateInit()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Salute"))
        {
            return false;
        }
        return true;
    }

    //行動時のモーション
    private bool UpdateRun()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Walking@loop"))
        {
            transform.position += transform.forward * Speed;
        }
        if (HitPoint <= 0)
        {
            Anime.SetTrigger("Down");
            audio.clip = DamageVoice;
            audio.Play();
        }
        else
        {
            return false;
        }
        return true;
    }

    //終了時のモーション
    private bool UpdateEnd()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("GoDown") || Anime.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return false;
        }
        return true;
    }

    //衝突判定
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (Flag == 1)
            {
                HitPoint--;
            }
            if (Anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Anime.SetTrigger("Damage");
            }
        }
    }
}
