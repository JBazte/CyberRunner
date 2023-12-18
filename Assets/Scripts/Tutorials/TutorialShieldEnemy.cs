using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialShieldEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ShieldTutorial();
    }
}