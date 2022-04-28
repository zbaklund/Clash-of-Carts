using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTransform : MonoBehaviour
{
    [Header("Info")]
	private Vector3 _startPos;
	private float _timer;
	private Vector3 _randomPos;

	[Header("Settings")]
	[Range(0f, 2f)]
	public float _time = 0.2f;
	[Range(0f, 2f)]
	public float _distance = 0.1f;
	[Range(0f, 0.1f)]
	public float _delayBetweenShakes = 0f;

	private void Awake()
	{
		_startPos = transform.position;
		Invoke("BeginShake", (Random.Range(0, 10)));
	}

	private void OnValidate()
	{
		if (_delayBetweenShakes > _time)
			_delayBetweenShakes = _time;
	}

	public void BeginShake()
	{
		StopAllCoroutines();
		StartCoroutine(Shake());
		Invoke("BeginShake", (Random.Range(0, 10)));
	}

	void OnCollisionEnter(Collision col){
        BeginShake();
    }

	private IEnumerator Shake()
	{
		_timer = 0f;

		while (_timer < _time)
		{
			_timer += Time.deltaTime;

			_randomPos = _startPos + (Random.insideUnitSphere * _distance);

			transform.position = _randomPos;

			if (_delayBetweenShakes > 0f)
			{
				yield return new WaitForSeconds(_delayBetweenShakes);
			}
			else
			{
				yield return null;
			}
		}

		transform.position = _startPos;
	}
}
