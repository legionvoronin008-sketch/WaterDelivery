using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishActive : MonoBehaviour
{
	public GameObject finish;
	public GameObject Sign;
	void Start()
	{
		finish.SetActive(false);
		Sign.SetActive(false);
	}
   void Update()
   {
	   if (Constants.WaterGeneratorFlag == true && Constants.WaterGeneratorFlag1 == true && Constants.WaterGeneratorFlag2 == true)
	   {
		   finish.SetActive(true);
		   Sign.SetActive(true);

	   }
   }
}
