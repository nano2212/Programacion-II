using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] Image lifeUI;
    float lifePlayer;
    int livesPlayer;
    List<EnemyBase> enemies;
    int totalEnemies;
    float maxLife;
    bool forthebegin = true;

    private void Start()
    {
        livesPlayer = GameManager.instance.player.lives;
        lifePlayer = GameManager.instance.player.life.Life;
        enemies = EnemyManager.instance.enemies;
    }

    private void LateUpdate()
    {
        if (forthebegin)
        {
            maxLife = lifePlayer;
            totalEnemies = EnemyManager.instance.enemies.Count;
            forthebegin = false;
        }
        livesUI.text = livesPlayer.ToString();
        enemiesUI.text = enemies.Count.ToString() + " / " + totalEnemies.ToString();
        lifeUI.fillAmount = GameManager.instance.player.life.Life / maxLife;
    }
}
