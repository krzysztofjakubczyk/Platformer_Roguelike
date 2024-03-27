using Cinemachine;
using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("head")]
    [SerializeField] CinemachineVirtualCamera[] cameras;

    public CameraManager Instance;
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTIme = 0.25f;
    public float _fallSpeedYDampingChangeTreshold = -15f;

    public bool IsLerpingY { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private CinemachineFramingTransposer _transposer;
    private CinemachineVirtualCamera _actualCamera;
    private float _normYPanAmount;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].enabled)
            {
                _actualCamera = cameras[i];
                _transposer = _actualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        _normYPanAmount = _transposer.m_YDamping;
    }
    #region Lerp the Y Damping
    public void LerpY(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingY = true;
        float startDmapAmount = _transposer.m_YDamping;
        float endDampAmount = 0f;
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }
        float elapsedTime = 0;
        while(elapsedTime < _fallYPanTIme)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDmapAmount, endDampAmount, (elapsedTime/ _fallYPanTIme));
            _transposer.m_YDamping = lerpedPanAmount;
            yield return null; 
        }
        IsLerpingY = false;
    }
    #endregion
}
