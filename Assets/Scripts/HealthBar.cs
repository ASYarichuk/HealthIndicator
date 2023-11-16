using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Image _healttBar;
    [SerializeField] private Image _healttBarSmooth;

    [SerializeField] private TMP_Text _healttBarText;

    private float _currentHealthBar;

    private float _timeHealthBarChange = 2f;
    private int _currentHealth;
    private int _maxHealth;

    private void Update()
    {
        _currentHealth = _player.CurrentHealth;
        _maxHealth = _player.MaxHealth;

        _currentHealthBar = (float)_currentHealth / _maxHealth;

        ShowHealth();

        if (_healttBarSmooth.fillAmount != _currentHealthBar)
        {
            _healttBarSmooth.fillAmount = Mathf.Lerp(_healttBarSmooth.fillAmount, _currentHealthBar, _timeHealthBarChange * Time.deltaTime);
        }
    }

    private void ShowHealth()
    {
        _healttBarText.text = $"Текущее здоровье: {_currentHealth} / {_maxHealth}";
        _healttBar.fillAmount = (float)_currentHealthBar;
    }
}
