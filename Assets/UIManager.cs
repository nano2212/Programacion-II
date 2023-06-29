using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Image lifeUI;
    float lifePlayer;
    List<EnemyBase> enemies;
    int totalEnemies;
    float maxLife;
    bool forthebegin = true;

    private void Start()
    {
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
        text.text = enemies.Count.ToString() + " / " + totalEnemies.ToString();
        lifeUI.fillAmount = GameManager.instance.player.life.Life / maxLife;
        Debug.Log(lifeUI.fillAmount);
    }
}
