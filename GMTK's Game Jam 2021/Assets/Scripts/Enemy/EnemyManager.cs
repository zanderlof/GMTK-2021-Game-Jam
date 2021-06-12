using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BasicEnemyStateMachine[] enemy;

    [Space]
    [SerializeField] bool Alarm = false;

    private void Update()
    {

    }

    public void ISaw()
    {
        foreach (BasicEnemyStateMachine enem in enemy)
        {
            if (enem.state != BasicEnemyStateMachine.enemyState.Shoot)
            {
                enem.state = BasicEnemyStateMachine.enemyState.Shoot;
            }
        }
    }
}
