using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {
    private PlayerController playerController;
    private Animator Enemy;

    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Player")) {
            return;
        }
        if (!playerController.GetInvulneravility()) {
            Debug.Log(col.gameObject.name);
            if (playerController.GetMotoActive()) {
                playerController.MotorbikeCrashed();
            } else {
                playerController.OnPlayerColliderHit(col.collider);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoin();
            other.gameObject.SetActive(false);
            return;
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpEffect powerUp = other.gameObject.GetComponent<PowerUp>().GetPowerUpEffect();
            if(!powerUp.GetIsAlreadyActive())
            {
                powerUp.ExecuteAction(playerController.gameObject);
                StartCoroutine(powerUp.StartCountDown());
            }
            else
            {
                StopCoroutine(powerUp.StartCountDown());
                StartCoroutine(powerUp.StartCountDown());
            }
            other.gameObject.SetActive(false);
            Debug.Log("COGIDO : " + powerUp);
            return;
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy = other.gameObject.GetComponentInChildren<Animator>();
            Enemy.Play("Die");
            other.gameObject.GetComponent<EnemyAbstract>().Invoke("Die",2f);
        }
        else if (!playerController.GetInvulneravility())
        {
            Debug.Log(other.gameObject.name);
            if (playerController.GetMotoActive())
            {
                playerController.MotorbikeCrashed();
            }
            else
            {
                playerController.OnPlayerColliderHit(other.GetComponent<Collider>());
            }
        }
    }
}