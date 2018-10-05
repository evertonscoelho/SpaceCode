﻿using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    private WebCamTexture cam;

	 public PhoneCamera(RawImage cameraImage) {
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            throw new System.InvalidOperationException(Messages.NENHUMA_CAMERA_ENCONTRADA);
        }

        for(int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                cam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(cam == null)
        {
            throw new System.InvalidOperationException(Messages.NAO_ABRIU_CAMERA);
        }
        
        cam.Play();
        cameraImage.texture = cam;
    }
	
    public byte[] TakePhoto()
    {
        Texture2D photo = new Texture2D(cam.width, cam.height);
        photo.SetPixels(cam.GetPixels());
        photo.Apply();
        byte[] bytes = photo.EncodeToPNG();
        cam.Stop();

        return bytes;
    }

}