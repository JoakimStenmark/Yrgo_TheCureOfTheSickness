//Robbans ful test

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerHP : MonoBehaviour
{
    public int hp = 2;
    // Start is called before the first frame update
    public void RemoveHP()
    {
        hp--;
        if(hp <= 0)
        {
            KillMe();
        }
    }

    void KillMe()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        print("GAME OVER");
    }

    // Update is called once per frame
    public void AddHp()
    {
        hp++;
    }
}
