using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BGCore;
using BGCore.Data;

public class Notifications : MonoBehaviour
{
    public string[] colors;

    public Text notificationBox;
    public Text specsBox;
    private string notifications = "";
    private string specs;
    private string GetSystemInfo()
    {
        string cpu;
        string cpuSpeed;
        string gpu;
        string gpuDriver;
        string gpuMem;
        string os;

        cpu = "CPU Type: " + SystemInfo.processorType + "\n";
        cpuSpeed = "CPU Speed: " + SystemInfo.processorFrequency.ToString() + " MHz\n";
        gpu = "GPU Vendor: " + SystemInfo.graphicsDeviceName + "\n";
        gpuDriver = "GPU API: " + SystemInfo.graphicsDeviceVersion + "\n";
        gpuMem = "GPU Memory: " + SystemInfo.graphicsMemorySize + " MB\n";
        os = "OS: " + SystemInfo.operatingSystem + "\n";

        return "Created by Grady Lorenzo\nDiscord: Nyxton#6759\n" + cpu + cpuSpeed + gpu + gpuDriver + gpuMem + os;
    }

    public void Awake()
    {
        Notify.ENotifyLog += ENotifyLog;
        Notify.Log(Notify.Intent.Success, "Notify.Log Started!");

        specs = GetSystemInfo();
        specsBox.text = specs;
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
        notificationBox.text = notifications;
    }
}
