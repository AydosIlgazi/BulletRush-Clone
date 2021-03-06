﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpawnerType { topSpawner, rightSpawner, bottomSpawner,leftSpawner };

public class EnemySpawner : MonoBehaviour
{
    private Vector3 offSetHorizontalRight;
    private Vector3 offSetHorizontalLeft;
    private Vector3 offSetVerticalUp;
    private Vector3 offSetVerticalDown;
    private Vector3 yCoordFixer;
    private float offSet = 0.5f;
    [SerializeField] GameObject Enemy=default;
    GameManager gameManager;

    public SpawnerType spawnerType;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        offSetHorizontalRight = new Vector3(offSet, 0f, 0f);
        offSetHorizontalLeft = new Vector3(-offSet, 0f, 0f);
        offSetVerticalUp = new Vector3(0f, 0f, offSet);
        offSetVerticalDown = new Vector3(0f, 0f, -offSet);
        yCoordFixer = new Vector3(0f, -5f, 0f);
        StartCoroutine(InstantiateEnemies());
    }

    IEnumerator InstantiateEnemies()
    {

        for (int i = 0; i < gameManager.SpawnCountPerWave; i++)
        {
            var enemy = Instantiate(Enemy, transform);
            gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
            int k = 1;
            Vector3 topScale = transform.position + yCoordFixer; ;
            Vector3 rightScale = transform.position+yCoordFixer;
            Vector3 leftScale = transform.position+yCoordFixer;
            Vector3 bottomScale = transform.position+yCoordFixer;
            for (int j = 0; j < gameManager.EnemyCountPerSpawn; j++)
            {
                if (k + 1 > gameManager.MaximumEnemySpawnedSimultenous)
                {
                    k = 1;
                    topScale.z += offSetVerticalUp.z;
                    rightScale.x += offSetHorizontalRight.x;
                    leftScale.x += offSetHorizontalLeft.x;
                    bottomScale.z += offSetVerticalDown.z;
                }
                if (spawnerType == SpawnerType.topSpawner)
                {
                    enemy = Instantiate(Enemy, topScale + (offSetHorizontalRight * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                    enemy = Instantiate(Enemy, topScale + (offSetHorizontalLeft * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                }
                else if (spawnerType == SpawnerType.rightSpawner)
                {
                    enemy = Instantiate(Enemy, rightScale + (offSetVerticalUp * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                    enemy = Instantiate(Enemy, rightScale + (offSetVerticalDown * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                }
                else if (spawnerType == SpawnerType.leftSpawner)
                {
                    enemy = Instantiate(Enemy, leftScale + (offSetVerticalUp * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                    enemy = Instantiate(Enemy, leftScale + (offSetVerticalDown * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                }
                else
                {
                    enemy = Instantiate(Enemy, bottomScale + (offSetVerticalUp * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                    enemy = Instantiate(Enemy, bottomScale + (offSetVerticalDown * (k)), Quaternion.identity, transform);
                    gameManager.Player.GetComponent<Player>().AddEnemy(enemy.GetComponent<Enemy>());
                }
                k++;
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
