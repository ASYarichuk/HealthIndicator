using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Image _healttBar;
    [SerializeField] private Image _healttBarSmooth;

    [SerializeField] private TMP_Text _healttBarText;

    private float _timeHealthBarChange = 2f;

    private void Awake()
    {
        _player.HealthChanged += ChangeHealth;
    }

    private void OnDestroy()
    {
        _player.HealthChanged -= ChangeHealth;
    }

    private void ChangeHealth(float percent, int currentHealth, int maxHealth)
    {
        _healttBar.fillAmount = percent;
        _healttBarText.text = $"Текущее здоровье: {currentHealth} / {maxHealth}";

        if (_healttBarSmooth.fillAmount != percent)
        {
            _healttBarSmooth.fillAmount = Mathf.Lerp(_healttBarSmooth.fillAmount, percent, _timeHealthBarChange * Time.deltaTime);
        }
    }
}
