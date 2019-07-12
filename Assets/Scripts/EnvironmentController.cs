using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentController : MonoBehaviour
{
    [Header("Environment settings")]
    [SerializeField] private bool enableDayNight;
    [Range(60, 3600)]
    [SerializeField] private int dayNightCycleSeconds;
    [Range(0f, 1f)]
    [SerializeField] private float nightToDayDuration;
    [Range(0.1f, 1f)]
    [SerializeField] private float sunAltitude;

    [Range(-360,360)]
    [SerializeField] private int cameraDirection;

    [Space(15)]
    [Header("Components")]
    [SerializeField] private Light mainLight;
    [SerializeField] private Transform board;

    private Transform mLightTransform;

    private void Awake()
    {
        
    }

    private void Start()
    {
        mLightTransform = mainLight.transform ?? null;
    }

    void Update()
    {
        UpdateBoardRotation();
    }

    private void UpdateBoardRotation()
    {
        board.rotation = Quaternion.Euler(
            board.rotation.eulerAngles.x,
            cameraDirection,
            board.rotation.eulerAngles.z);
    }
}
