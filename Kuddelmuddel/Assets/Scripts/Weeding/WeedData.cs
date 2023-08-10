using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedData : MonoBehaviour
{
    public int testNum = 0;
    public string testString = "Default";
    public int growthState = 0; //used to determine which animation to display?
    public float growthRate = 100;
    public float spreadRate = 100;
    public int health = 100;
    public bool canGrow = true;
    public bool isGrown = false;
    public bool isGrowing = true;
    public float totalGrowth = 0; //incremental growth 0 - 100. Grows 5% a second at growth rate of 100? 5 = 5%


}
