using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Image _instantHealthChanger;
    [SerializeField] private Image _smoothHealthChanger;

    [SerializeField] private TMP_Text _textDisplayHealth;

    private float _timeHealthBarChange = 2f;

    private void Awake()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float percent, int currentHealth, int maxHealth)
    {
        _instantHealthChanger.fillAmount = percent;
        _textDisplayHealth.text = $"Текущее здоровье: {currentHealth} / {maxHealth}";

        if (_smoothHealthChanger.fillAmount != percent)
        {
            _smoothHealthChanger.fillAmount = Mathf.Lerp(_smoothHealthChanger.fillAmount, percent, _timeHealthBarChange * Time.deltaTime);
        }
    }
}
