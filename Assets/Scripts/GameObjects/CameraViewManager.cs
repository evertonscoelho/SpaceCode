public class CameraViewManager : UnityEngine.MonoBehaviour
{

	void Start () {
        deactivate();
        PictureManager.instance.setCameraViewManager(this);
    }

    public void active()
    {
        gameObject.SetActive(true);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}
