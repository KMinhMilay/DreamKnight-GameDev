using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public float mana;
    float maxMana =100;
    public Image manaBar;
    float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mana=maxMana;


    }

    // Update is called once per frame
    void Update()
    {
        if (mana<0) mana=0;
        if (mana > maxMana) mana = maxMana;
        lerpSpeed = 3f * Time.deltaTime;
        ManaBarFiller();
        ManaRecover();
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    print("Đã nhấn nút K");
        //    if (mana > 0)
        //    {
        //        print("Đã tung Skill");
        //        mana -= 20;
        //    }
        //}
    }
    
    void ManaBarFiller()
    {
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, mana / maxMana, lerpSpeed);


    }
    void ManaRecover()
    {
        if(mana < maxMana)
        {
            mana += 0.01f;
            print("Đang hồi phục mana");
        }
        
    }
    void GetMana(float manaPoint)
    {
        if (mana > 0)
        {
            print("Đã tung Skill");
            mana -= manaPoint;
            print("Đã trừ mana");
        }
    }

}
