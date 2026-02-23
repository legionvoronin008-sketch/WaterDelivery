using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    // Start is called before the first frame update
     #region Movement
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
    public const string JUMP = "Jump";
    public const string FIRE_1 = "Fire1";
    public static bool WaterGeneratorFlag = false;
    public static bool WaterGeneratorFlag1 = false;
    public static bool WaterGeneratorFlag2 = false;
    public static bool TiredPerson = false;
    public static bool TiredPerson1 = false;
    public static bool TiredPerson2 = false;
    public static int SavedPeoplenumber = 0;
    public static int DeathCount = 0;
    #endregion
}