using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    private Subscription<ChangeEnemyCount> enemyCountSub;
    private string text_to_display = "ENEMIES REMAINING: ";
    public TextMeshProUGUI tmp;
    public GameObject enemy_parent;
    // Start is called before the first frame update
    void Start()
    {
        enemyCountSub = EventBus.Subscribe<ChangeEnemyCount>(_updateText);
        tmp.SetText(text_to_display + enemy_parent.transform.childCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _updateText(ChangeEnemyCount e) {
        tmp.SetText(text_to_display + e.new_count.ToString());
    }
}
