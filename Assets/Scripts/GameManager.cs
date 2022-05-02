using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy_parent;
    private int num_enemeies = 0;
    private int round_num;
    public GameObject spawn_locations;
    Subscription<EnemyDeathEvent> e_death;
    Subscription<PlayerDeathEvent> p_death;
    Subscription<EnemySpawnEvent> e_spawn;
    // Start is called before the first frame update
    void Start()
    {
        e_death = EventBus.Subscribe<EnemyDeathEvent>(_enemyDeath);
        e_spawn = EventBus.Subscribe<EnemySpawnEvent>(_enemySpawn);
        p_death = EventBus.Subscribe<PlayerDeathEvent>(_playerDeath);
        round_num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _enemyDeath(EnemyDeathEvent e) {
        num_enemeies -= 1;
        EventBus.Publish<ChangeEnemyCount>(new ChangeEnemyCount(num_enemeies));
        if (num_enemeies == 0) {
            // Next round? Game Over?
        }
    }

    void _enemySpawn(EnemySpawnEvent e) {
        Debug.Log("Adding enemies");
        num_enemeies += 1;
    }

    void _playerDeath(PlayerDeathEvent e) {
        // Do something
        SceneManager.LoadScene(0);
    }

    private void OnDestroy() {
        EventBus.Unsubscribe(e_spawn);
        EventBus.Unsubscribe(e_death);
    }
}
