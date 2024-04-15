using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarControl : MonoBehaviour
{
    [SerializeField] Image _blueImage;
    [SerializeField] Image _yellowImage;
    [SerializeField] StaminaControl _staminaController;
    [SerializeField] StaminaBarControl _staminaBarController;
    [SerializeField] float _stamina;
    [SerializeField] float _maxStamina;
    [Range(0f, 3f)][SerializeField] float _timeForRedImage;

    private void Start()
    {
        _stamina = _staminaController.GetStamina();
        _maxStamina = _staminaController.GetMaxStamina();
        healthController.DiscardHP += setLessHP;
    }

    private void setLessHP()
    {
        _stamina = _staminaController.GetStamina();
        _maxStamina = _staminaController.GetMaxStamina();
        _blueImage.fillAmount = _stamina / _maxStamina;
        StartCoroutine(YellowImageNotVisible());
    }

    private IEnumerator YellowImageNotVisible()
    {
        yield return new WaitForSeconds(_timeForRedImage);
        _yellowImage.fillAmount = _stamina / _maxStamina;
    }
}
