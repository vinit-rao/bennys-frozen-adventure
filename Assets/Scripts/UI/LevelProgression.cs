using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public Animator truckAnimator;

    //Call this when a level is completed
    public void MoveTruckToNextLevel(int levelCompleted)
    {
        switch(levelCompleted)
        {
            case 1:
                truckAnimator.SetTrigger("SunnySundaeToSandyScoop");
                break;
            case 2:
                truckAnimator.SetTrigger("SandyScoopToNebulaSwirl");
                break;
           
        }
    }
}
