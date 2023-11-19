using System.Collections;
using UnityEngine;
using System;

public class Player : Character
{
    [SerializeField] private int _damage = 2;

    private int _currentHealth = 2;
    private int _maxHealth = 10;

    private Enemy _enemy;

    private float _percent;

    public event Action<float, int, int> HealthChanged;

    private void Awake()
    {
        _currentHealth = Health;
    }

    private void Update()
    {
        _currentHealth = Health;
        _percent = (float)_currentHealth / _maxHealth;
        HealthChanged?.Invoke(_percent, _currentHealth, _maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.GetComponent<Enemy>())
        {
            _enemy = enemy.gameObject.GetComponent<Enemy>();
            StartCoroutine(CauseDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D enemy)
    {
        if (enemy.gameObject.GetComponent<Enemy>())
        {
            StopCoroutine(CauseDamage());
            _enemy = null;
        }
    }

    private IEnumerator CauseDamage()
    {
        var waitForOneSecond = new WaitForSeconds(1.0f);

        while (_enemy != null)
        {
            _enemy.TakeDamage(_damage);
            yield return waitForOneSecond;
        }
    }

    public void TakeAidKit(int amountHealth)
    {
        if (_currentHealth + amountHealth >= _maxHealth)
        {
            AddHealth(_maxHealth - _currentHealth);
        }
        else
        {
            AddHealth(amountHealth);
        }
    }
}
