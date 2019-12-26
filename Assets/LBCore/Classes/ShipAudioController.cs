using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGCore;

public class ShipAudioController : MonoBehaviour
{
    public AnimationCurve pitchCurve;

    private AudioSource source;
    private ShipDynamics sd;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        sd = GetComponent<ShipDynamics>();
    }

    private void Update()
    {
        source.pitch = pitchCurve.Evaluate(sd.CurrentVelocity / Constants.SpeedLimit);
    }
}
