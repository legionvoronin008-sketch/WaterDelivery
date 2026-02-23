using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
   public int disable_number;
   public Animator anim;
   public GameObject Trigger;
   public GameObject PTsystem;

    // Update is called once per frame
    void Update()
    {
        if (Constants.WaterGeneratorFlag == true && disable_number == 2)
        {
            Instantiate(PTsystem ,transform.position, Quaternion.identity);
            anim.SetBool("Activated",true);
            Trigger.SetActive(false);
        }
        else if (Constants.WaterGeneratorFlag1 == true && disable_number == 1)
        {
            Instantiate(PTsystem ,transform.position, Quaternion.identity);
            anim.SetBool("Activated",true);
            Trigger.SetActive(false);
        }
        else if (Constants.WaterGeneratorFlag2 == true && disable_number == 3)
        {
            Instantiate(PTsystem ,transform.position, Quaternion.identity);
            anim.SetBool("Activated",true);
            Trigger.SetActive(false);
        }
        
    }
}
