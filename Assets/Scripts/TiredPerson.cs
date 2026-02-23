using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredPerson : MonoBehaviour
{
   public int disable_number;
   public Animator anim;
   public GameObject Trigger;
   public GameObject BottlePrefab;
   public Transform Point;

    // Update is called once per frame
    void Update()
    {
        if (Constants.TiredPerson == true && disable_number == 0)
        {
            Instantiate(BottlePrefab, Point.position, Quaternion.identity);
            anim.SetBool("GotWater",true);
            Trigger.SetActive(false);
        }
        else if (Constants.TiredPerson1 == true && disable_number == 1)
        {
            Instantiate(BottlePrefab, Point.position, Quaternion.identity);
            anim.SetBool("GotWater",true);
            Trigger.SetActive(false);
        }
        else if (Constants.TiredPerson2 == true && disable_number == 2)
        {
            Instantiate(BottlePrefab, Point.position, Quaternion.identity);
            anim.SetBool("GotWater",true);
            Trigger.SetActive(false);
        }
  
        
    }
}
