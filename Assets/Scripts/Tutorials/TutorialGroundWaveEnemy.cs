using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialGroundWaveEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.GroundWaveTutorial();
    }
}