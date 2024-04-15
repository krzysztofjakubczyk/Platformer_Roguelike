using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Image _greenImage;
    [SerializeField] Image _redImage;
    [SerializeField] healthController _healthController;
    [SerializeField] HealthBarController _healthBarController;
    [SerializeField] float _health;
    [SerializeField] float _maxHealth;
    [Range(0f,3f)][SerializeField] float _timeForRedImage;

    private void Start()
    {
        _health = _healthController.GetHealth();
        _maxHealth = _healthController.GetMaxHealth();
        healthController.DiscardHP += setLessHP;
    }

    private void setLessHP()
    {
        _health = _healthController.GetHealth();
        _maxHealth = _healthController.GetMaxHealth();
        _greenImage.fillAmount = _health / _maxHealth;
        StartCoroutine(RedImageNotVisible());
    }

    private IEnumerator RedImageNotVisible()
    {
        yield return new WaitForSeconds(_timeForRedImage);
        _redImage.fillAmount = _health / _maxHealth;
    }
}
