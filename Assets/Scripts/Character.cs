using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health = 3;

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(int amountHealth)
    {
        _health += amountHealth;
    }
}
