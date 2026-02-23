using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
   public Transform[] layers;
   public float[] coeff;

   private Vector3 [] startPos;
   private Transform cam;

   void Start()
   {
       cam = Camera.main.transform;
       startPos = new Vector3 [layers.Length];
       for (int i = 0; i<layers.Length; i++)
       {
           startPos[i] = layers[i].position;
       }
   }

    // Update is called once per frame
   void Update()
   {
       for (int i = 0; i< layers.Length; i++)
       {
           Vector3 camDelta = cam.position;
           layers[i].position = startPos[i] + camDelta * coeff[i];
       }
   }
}
