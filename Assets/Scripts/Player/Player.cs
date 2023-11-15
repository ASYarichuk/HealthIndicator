using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Character
{
    [SerializeField] private int _damage = 2;

    [SerializeField] private Image _healttBar;
    [SerializeField] private Image _healttBarSmooth;

    [SerializeField] private TMP_Text _healttBarText;

    private int _currentHealth = 2;
    private int _maxHealth = 10;
    private int _timeHealthBarChange = 2;

    private float _currentHealthBar;

    private Enemy _enemy;

    private void Update()
    {
        _currentHealth = Health;
        _currentHealthBar = (float)_currentHealth / _maxHealth;

        ShowHealth();

        if (_healttBarSmooth.fillAmount != _currentHealthBar)
        {
            _healttBarSmooth.fillAmount = Mathf.Lerp(_healttBarSmooth.fillAmount, _currentHealthBar, _timeHealthBarChange * Time.deltaTime);
        }
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

    private void ShowHealth()
    {
        _healttBarText.text = $"Текущее здоровье: {_currentHealth} / {_maxHealth}";
        _healttBar.fillAmount = (float)_currentHealthBar;
    }
}
