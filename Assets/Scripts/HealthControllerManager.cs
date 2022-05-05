using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthControllerManager : MonoBehaviour
{
    public GameObject player;
    private Subscription<PlayerHitEvent> hit_event;
    private string text_to_display = "HEALTH: ";
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hit_event = EventBus.Subscribe<PlayerHitEvent>(_hitEventHandler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _hitEventHandler(PlayerHitEvent e) {
        float health = player.GetComponent<Health>().health;
        tmp.SetText(text_to_display + health.ToString());
    }

    private void OnDestroy() {
        EventBus.Unsubscribe<PlayerHitEvent>(hit_event);
    }
}
