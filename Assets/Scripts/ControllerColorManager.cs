using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerColorManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer cube_ren;
    [SerializeField] private MeshRenderer ball_ren;

    Subscription<PlayerHitEvent> hit_event;
    Subscription<GrabbedInvincibilityEvent> inv_event;
    // Start is called before the first frame update
    void Start()
    {
        hit_event = EventBus.Subscribe<PlayerHitEvent>(_playerHitHelper);
        inv_event = EventBus.Subscribe<GrabbedInvincibilityEvent>(_invGrabHelper);
    }

    private void _playerHitHelper(PlayerHitEvent e) {
        StartCoroutine(_playerHit());
    }

    private IEnumerator _playerHit() {
        cube_ren.material.SetColor("_Color", Color.red);
        ball_ren.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(2.0f);
        setColorWhite();
    }
    void _invGrabHelper(GrabbedInvincibilityEvent e) {
        StartCoroutine(_invGrab());
    }
    private IEnumerator _invGrab() {
        cube_ren.material.SetColor("_Color", Color.yellow);
        ball_ren.material.SetColor("_Color", Color.yellow);
        yield return new WaitForSeconds(5.0f);
        setColorWhite();
    }

    private void setColorWhite() {
        cube_ren.material.SetColor("_Color", Color.white);
        ball_ren.material.SetColor("_Color", Color.white);
    }

    void OnDestroy() {
        EventBus.Unsubscribe(hit_event);
        EventBus.Unsubscribe(inv_event);
    }
}
