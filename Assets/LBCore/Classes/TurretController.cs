using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LBCore;

[RequireComponent(typeof(Animator))]
public class TurretController : MonoBehaviour
{
    #region fields
    public Transform Target;

    public Transform TurretBase;
    public Transform TurretBarrel;

    [Serializable]
    public struct TurretAttributes
    {
        public string id;
        public HardpointSize Size;
        public float LookSpeed;
        public float FireInterval;
        public Color EffectColor;
    }

    public TurretAttributes Attributes;

    private Animator anim;
    private AudioSource asource;
    #endregion

    #region MB Methods
    private void Start()
    {
        anim = GetComponent<Animator>();
        InitializeEffects();
    }

    private void Update()
    {
        DoTurretRotation();

        DoFireLoop();
    }
    #endregion

    #region Rotation
    private bool useSmoothMovement = false;
    private bool isAligned
    {
        get
        {
            if (Target != null)
            {
                if (Vector3.Angle(TurretBarrel.rotation.eulerAngles, Quaternion.LookRotation(Target.position - TurretBarrel.position).eulerAngles) < 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    private Vector3 targetPos
    {
        get
        {
            if (Target != null)
            {
                return Target.transform.position;
            }
            else
            {
                return new Vector3(0, 0, 1);
            }
        }
    }
    private float MinBarrelAngle = -180;
    private float MaxBarrelAngle = 0;

    private void DoTurretRotation()
    {
        if (TurretBase != null && TurretBarrel != null)
        {
            if (Target != null)
            {
                doBaseRotation();
                doBarrelRotation();
            }
        }
        else
        {
            Debug.LogWarning("One of the variables of TurretController has not been assigned.");
        }
    }

    private void doBaseRotation()
    {
        var directionToTarget = (targetPos - transform.position).normalized;
        var worldAxis = transform.up;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, worldAxis);

        Quaternion rotation;
        if (useSmoothMovement)
        {
            rotation = Quaternion.RotateTowards(TurretBase.rotation, Quaternion.LookRotation(flattenedDirection, worldAxis), Attributes.LookSpeed);
        }
        else
        {
            rotation = Quaternion.LookRotation(flattenedDirection, worldAxis);
        }
        TurretBase.rotation = rotation;
    }

    private void doBarrelRotation()
    {
        var directionToTarget = (targetPos - TurretBarrel.position).normalized;
        var axis = TurretBase.right;
        var flattenedDirection = Vector3.ProjectOnPlane(directionToTarget, axis);
        var signedAngle = Vector3.SignedAngle(TurretBase.forward, flattenedDirection, axis);
        signedAngle = Mathf.Clamp(signedAngle, MinBarrelAngle, MaxBarrelAngle);

        Quaternion rotation;

        if (useSmoothMovement)
        {
            rotation = Quaternion.RotateTowards(TurretBarrel.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
                TurretBarrel.up), Attributes.LookSpeed);
        }
        else
        {
            rotation = Quaternion.LookRotation(Quaternion.AngleAxis(signedAngle, axis) * TurretBase.forward,
            TurretBarrel.up);
        }
        TurretBarrel.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.forward * 2));
        Gizmos.DrawRay(new Ray(TurretBase.position, TurretBase.forward * 2));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.right * 2));
        Gizmos.DrawRay(new Ray(TurretBase.position, TurretBase.right * 2));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(TurretBarrel.position, TurretBarrel.up * 5));
    }
    #endregion

    #region Firing
    //Please note, this is only a visual effect. No actual damage messages are sent from here.

    public LineRenderer[] effects;
    private Vector3[] effectInitialPositions;
    private bool _isActive;
    public bool isActive
    {
        get { return _isActive; }
        private set { _isActive = value; StopCoroutine("Fire"); }
    }
    private float nextFireTime;

    private void InitializeEffects()
    {
        if(GetComponent<AudioSource>() != null)
        {
            asource = GetComponent<AudioSource>();
        }

        if(effects.Length > 0)
        {
            effectInitialPositions = new Vector3[effects.Length];
            for(int i = 0; i < effects.Length; i++)
            {
                effectInitialPositions[i] = effects[i].GetPosition(0);
                effects[i].startColor = Attributes.EffectColor;
                effects[i].endColor = Attributes.EffectColor;
            }
        }
    }

    private void DoFireLoop()
    {
        for(int i = 0; i < effects.Length; i++)
        {
            float length = Vector3.Distance(effects[i].transform.position, targetPos);
            effects[i].SetPosition(0, effectInitialPositions[i]);
            effects[i].SetPosition(1, Vector3.forward * length);
        }

        if (_isActive)
        {
            if(Time.time > nextFireTime)
            {
                StartCoroutine("Fire");
                nextFireTime = Time.time + Attributes.FireInterval;
            }
        }
    }

    private IEnumerator Fire()
    {
        float d = UnityEngine.Random.Range(0, Attributes.FireInterval / 2);
        yield return new WaitForSeconds(d);
        if (isAligned)
        {
            anim.Play("default");
            anim.Play("firing");
            if(asource)
            {
                asource.PlayOneShot(asource.clip);
            }
        }
    }

    #endregion

    #region Public Methods

    public void ToggleFiring()
    {
        isActive = !isActive;
    }

    public void SetFiring(bool b)
    {
        isActive = b;
    }

    public void SetTarget (Transform t)
    {
        SetFiring(false);
        Target = t;
    }
    #endregion
}
