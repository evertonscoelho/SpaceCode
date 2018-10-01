using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    private WebCamTexture cam;

	 public PhoneCamera(RawImage cameraImage) {
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            throw new System.InvalidOperationException("Nenhuma câmera encontrada");
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
            throw new System.InvalidOperationException("Não foi possível abrir a câmera");
        }
        
        cam.Play();
        cameraImage.texture = cam;
    }
	
    public IEnumerator TakePhoto()
    {
        // NOTE - you almost certainly have to do this here:

        yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(cam.width, cam.height);
        photo.SetPixels(cam.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        print("HAHA");
        yield return null;

    }
}
