using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemy_parent;
    private int num_enemeies = 0;
    private int round_num;
    public GameObject enemy_prefab;
    public GameObject spawn_location_parent;
    private GameObject[] spawn_locations;
    Subscription<EnemyDeathEvent> e_death;
    Subscription<PlayerDeathEvent> p_death;
    Subscription<EnemySpawnEvent> e_spawn;
    // Start is called before the first frame update
    void Start()
    {
        e_death = EventBus.Subscribe<EnemyDeathEvent>(_enemyDeath);
        e_spawn = EventBus.Subscribe<EnemySpawnEvent>(_enemySpawn);
        p_death = EventBus.Subscribe<PlayerDeathEvent>(_playerDeath);
        round_num = 1;
        spawn_locations = new GameObject[spawn_location_parent.transform.childCount];
        for (int i = 0; i < spawn_locations.Length; ++i) {
            spawn_locations[i] = spawn_location_parent.transform.GetChild(i).gameObject;
        }
        beginRound();
    }

    void beginRound() {
        for (int i = 0; i < round_num; ++i) { // spawn as many enemies as the round number
            GameObject enemy = Instantiate(enemy_prefab, spawn_locations[i].transform.position, Quaternion.identity);
            enemy.transform.parent = enemy_parent.transform;
        }
    }

    void _enemyDeath(EnemyDeathEvent e) {
        num_enemeies -= 1;
        EventBus.Publish<ChangeEnemyCount>(new ChangeEnemyCount(num_enemeies));
        if (num_enemeies == 0) {
            ++round_num;
            beginRound();
        }
    }

    void _enemySpawn(EnemySpawnEvent e) {
        Debug.Log("Adding enemies");
        num_enemeies += 1;
        EventBus.Publish<ChangeEnemyCount>(new ChangeEnemyCount(num_enemeies));
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
