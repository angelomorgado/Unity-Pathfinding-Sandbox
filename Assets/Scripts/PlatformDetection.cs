using UnityEngine;
using UnityEngine.XR;

public class PlatformDetection : MonoBehaviour
{
    GameObject vrPlayer;
    GameObject pcPlayer;

    void Start()
    {
        vrPlayer = GameObject.Find("VrPlayer").gameObject;
        pcPlayer = GameObject.Find("Player").gameObject;

        Debug.Log("Platform: " + Application.platform);
        Debug.Log("XR Device: " + XRSettings.loadedDeviceName);

        if(XRSettings.loadedDeviceName == "oculus display")
        {
            Debug.Log("VR Player");
            vrPlayer.SetActive(true);
            pcPlayer.SetActive(false);
        }
        else
        {
            Debug.Log("PC Player");
            vrPlayer.SetActive(false);
            pcPlayer.SetActive(true);
        }
    }
}