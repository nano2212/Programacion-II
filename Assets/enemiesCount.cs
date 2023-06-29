using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class enemiesCount : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    List<EnemyBase> enemies;
    int totalEnemies;
    bool forthebegin = true;

    private void Start()
    {
        enemies = EnemyManager.instance.enemies;
        totalEnemies = EnemyManager.instance.enemies.Count;
    }

    private void LateUpdate()
    {
        if (forthebegin)
        {
            totalEnemies = EnemyManager.instance.enemies.Count;
            forthebegin = false;
        }
        text.text = enemies.Count.ToString() + " / " + totalEnemies.ToString();

    }
}
