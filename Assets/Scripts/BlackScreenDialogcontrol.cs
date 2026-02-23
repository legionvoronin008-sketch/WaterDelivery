using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BlackScreenDialogcontrol : MonoBehaviour
{
    public float Time1 = 2.0f;
    public float Time2 = 5.0f;
    public float Pause = 17.5f;
    public GameObject  BlackScreen1;
    public GameObject  BlackScreen2;
    public GameObject  BlackScreen3;
    public GameObject  BlackScreen4;
    public GameObject  DialogSheet;
    public GameObject  Dialog5;
    public GameObject  Dialog6;
    public GameObject  Dialog7;
    public GameObject  Dialog8;
    void Awake()
    {
        StartCoroutine(Dialog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Dialog()
    {
        BlackScreen1.SetActive(true);
        yield return new WaitForSeconds(Time1);
        BlackScreen1.SetActive(false);
        BlackScreen2.SetActive(true);
        yield return new WaitForSeconds(Time2);
        BlackScreen2.SetActive(false);
        BlackScreen3.SetActive(true);
        yield return new WaitForSeconds(Time2);
        BlackScreen3.SetActive(false);
        BlackScreen4.SetActive(true);
        yield return new WaitForSeconds(Time2);
        BlackScreen4.SetActive(false);
        yield return new WaitForSeconds(Pause);
        DialogSheet.SetActive(true);
        Dialog5.SetActive(true);
        yield return new WaitForSeconds(Time2);
        Dialog5.SetActive(false);
        Dialog6.SetActive(true);
        yield return new WaitForSeconds(Time2);
        Dialog6.SetActive(false);
        Dialog7.SetActive(true);
        yield return new WaitForSeconds(Time2);
        Dialog7.SetActive(false);
        Dialog8.SetActive(true);
        yield return new WaitForSeconds(Time2);
        Dialog8.SetActive(false);
        DialogSheet.SetActive(false);
    }
}
