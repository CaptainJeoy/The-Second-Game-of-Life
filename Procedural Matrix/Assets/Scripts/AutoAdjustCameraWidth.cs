using UnityEngine;

public class AutoAdjustCameraWidth : MonoBehaviour
{
	public SpriteRenderer widthReference;
	public Camera cam;

	void Awake ()
	{
		float widthReso = (widthReference.bounds.size.x * Screen.height) / Screen.width;

		cam.orthographicSize = widthReso / 2f;
	}
}
