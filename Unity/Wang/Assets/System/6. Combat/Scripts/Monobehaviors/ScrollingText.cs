using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class ScrollingText : MonoBehaviour
{
	public float Duration = 1f;
	public float Speed;
	
	private TextMesh textMesh;

	private void Awake()
	{
		textMesh = GetComponent<TextMesh>();
	}

	private void Start()
	{
		FaceCamera();
		StartCoroutine(Scroll());
	}

	public void SetText(string text, Color color, float scale = 1)
	{
		textMesh.text = text;
		textMesh.color = color;
		textMesh.fontSize = (int)(textMesh.fontSize * scale);
	}
	
	private void FaceCamera()
	{
		var rot = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
		transform.rotation = rot;
	}

	IEnumerator Scroll()
	{
		float startTime = Time.time;
		while (Time.time - startTime < Duration)
		{
			transform.Translate(Vector3.up * Speed * Time.deltaTime, Camera.main.transform);
			FaceCamera();
			yield return null;
		}
		
		Destroy(gameObject);
	}

}