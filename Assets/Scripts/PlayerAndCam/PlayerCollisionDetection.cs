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
                //col.gameObject.GetComponent<Collider>().enabled = false;
                ModuleManager.Instance.SetCollisionObject(col.gameObject);
                playerController.OnPlayerColliderHit(col.collider);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            GameManager.Instance.GameOver();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.AddCoin();
            other.gameObject.SetActive(false);
            SfxMusicManager.Instance.PlaySfxMusic("CoinSfx");
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
            return;
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAbstract>().Die();
        }
        else if (other.gameObject.CompareTag("Tutorial"))
        {
            //if(other.gameObject.GetComponent<TutorialSlashEnemy>() != null) UIManager.Instance.SlashTutorial();
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