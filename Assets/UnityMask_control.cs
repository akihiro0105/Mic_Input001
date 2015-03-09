using UnityEngine;
using System.Collections;

public class UnityMask_control : MonoBehaviour {

    public int HitPoint = 10;
    public float Speed = 0.01f;

    private Animator Anime;
    private int Flag = 0;
	// Use this for initialization
	void Start () {
        Anime = GetComponent<Animator>();
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
                Destroy(gameObject);
                break;
        }
	}

    private bool UpdateInit()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("Hello"))
        {
            return false;
        }
        return true;
    }

    private bool UpdateRun()
    {
        if (Anime.GetCurrentAnimatorStateInfo(0).IsName("WalkWing"))
        {
            transform.position += transform.forward * Speed;
        }
        if (HitPoint <= 0)
        {
            Anime.SetTrigger("Down");
        }
        else
        {
            return false;
        }
        return true;
    }

    private bool UpdateEnd()
    {
        if (!Anime.GetCurrentAnimatorStateInfo(0).IsName("Down") || Anime.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return false;
        }
        return true;
    }

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
