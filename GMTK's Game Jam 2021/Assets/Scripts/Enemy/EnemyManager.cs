using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<BasicEnemyStateMachine> enemiesInScene;
    [SerializeField] List<GameObject> enemiesToSpawn;
    [SerializeField] int enemySpawnThreshold;
    [SerializeField] Transform enemySpawnPoint;

    [Space]

    private void Update()
    {
        if (enemiesInScene.Count < enemySpawnThreshold && enemiesToSpawn.Count > 0)
        {
            GameObject newEnemy = Instantiate(enemiesToSpawn[0], enemySpawnPoint.position, enemySpawnPoint.rotation);
            newEnemy.transform.parent = transform;
            enemiesToSpawn.RemoveAt(0);
            enemiesInScene.Add(newEnemy.GetComponent<BasicEnemyStateMachine>());
            newEnemy.GetComponent<BasicEnemyStateMachine>().state = BasicEnemyStateMachine.enemyState.Shoot;
        }
    }

    public void ISaw()
    {
        foreach (BasicEnemyStateMachine enem in enemiesInScene)
        {
            if (enem.state != BasicEnemyStateMachine.enemyState.Shoot)
            {
                enem.state = BasicEnemyStateMachine.enemyState.Shoot;
            }
        }
    }

    public void IDied(BasicEnemyStateMachine me)
    {
        enemiesInScene.Remove(me);
    }
}
