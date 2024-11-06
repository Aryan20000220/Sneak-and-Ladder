using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    AudioSource _audio;
    public AudioClip _clip;
    Rigidbody rb;
    bool hasLanded = false;
    bool thrown = false;

    Vector3 initPos;

    public DiceSides[] diceSides;
    public int diceValue;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }

    public void RollDice()
    {
        Reset();

        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.mass = 1;
            rb.AddTorque(Random.Range(5500,8500),Random.Range(5500, 8500),Random.Range(5500, 8500));
        }
        else if(thrown && hasLanded)
        {
            Reset();
        }
    }

    void Reset()
    {
        transform.position = initPos;
        rb.isKinematic = false;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    void Update()
    {
        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            sideValueCheck();
            //print("first if");

        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            //print("second if");
            RollAgain();
        }        
    }

    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.mass = (Random.Range(2,6));
        rb.AddTorque(Random.Range(0,500),Random.Range(0,500),Random.Range(0,500));
        //print("Dice = 0");
    }

    void sideValueCheck()
    {
        diceValue = 0;
        foreach( DiceSides side in diceSides)
        {
            if(side.OnGround())
            {
                diceValue = side.sideValue;
                //print("SideVal Exec");
                GameManager.instance.RolledDiceNum(diceValue);

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        _audio.clip = _clip;
        _audio.Play();
    }
}
