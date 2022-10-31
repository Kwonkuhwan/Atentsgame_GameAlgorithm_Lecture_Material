using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Barrier : MonoBehaviour
{
    public float skillCoolTime = 10.0f;
    public float skillDuration = 3.0f;

    private GameObject skillEffect;
    private float currentCoolTime = 0.0f;
    private float currentDuration = 0.0f;

    private float CurrentDuration
    {
        get => currentDuration;
        set
        {
            currentDuration = value;
            onDurationTimeChange?.Invoke(skillDuration - currentDuration, skillDuration);
        }
    }

    public float CurrentCoolTime
    {
        get => currentCoolTime;
        set
        {
            currentCoolTime = value;
            onCoolTimeChange?.Invoke(currentCoolTime, skillCoolTime);
        }
    }

    public bool isSkillReady { get => (currentCoolTime <= 0.0f); }
    public bool isSkillActivate { get => skillEffect.activeSelf; }

    public Action<float, float> onCoolTimeChange;
    public Action<float, float> onDurationTimeChange;
    public Action<bool> OnDurationMode;

    private void Awake()
    {
        skillEffect = transform.GetChild(6).gameObject;
    }

    private void Start()
    {
        CurrentCoolTime = skillCoolTime;
    }

    private void Update()
    {
        if (isSkillActivate)
        {
            CurrentDuration += Time.deltaTime;
            if (CurrentDuration >= skillDuration)
            {
                skillEffect.SetActive(false);
                ResetCoolTime();
            }
        }
        else
        {
            CurrentCoolTime -= Time.deltaTime;
        }
    }

    public void ResetCoolTime()
    {
        OnDurationMode?.Invoke(false);
        CurrentCoolTime = skillCoolTime;
    }

    public void UseSkill()
    {
        if (CurrentCoolTime <= 0.0f)
        {
            skillEffect.SetActive(true);
            CurrentDuration = 0.0f;
            OnDurationMode?.Invoke(true);
        }
    }

}
