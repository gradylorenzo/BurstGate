using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BGCore;

[RequireComponent(typeof(Text))]
public class Notifications : MonoBehaviour
{
    public string[] colors;

    private float currentOffset;
    private float wantedOffset;
    private Text textBox;

    private string notifications;
    private string offset;
    private string delta;

    public void Awake()
    {
        Notify.ENotifyLog += ENotifyLog;
        GameManager.Events.EFloatingOriginOffsetUpdated += EFloatingOriginOffsetUpdated;
        GameManager.Events.EFloatingOriginOffsetDelta += EFloatingOriginOffsetDelta;
        textBox = GetComponent<Text>();
    }

    private void EFloatingOriginOffsetDelta(DoubleVector2 v)
    {
        delta = v.ToString("0.000");
    }

    private void EFloatingOriginOffsetUpdated(DoubleVector2 v)
    {
        offset = v.ToString("0.000");
    }

    private void Start()
    {
        Notify.Log(Notify.Intent.Success, "Notify.Log Started!");
    }

    private void ENotifyLog(Notify.Intent intent, string text)
    {
        string prefix = "<color=" + colors[(int)intent]+ ">";
        string main = text;

        string final = prefix + main + "</color>\n";
        notifications += final;

        string currentText = notifications;
        while (currentText.Length > 1000)
        {
            currentText = currentText.Substring(currentText.IndexOf("</color>") + 8);
        }
        notifications = currentText;
    }

    private void FixedUpdate()
    {
        textBox.text = notifications + "\n\nOffset:\n" + offset + "\n\nOffset Delta:\n" + delta;
    }
}
