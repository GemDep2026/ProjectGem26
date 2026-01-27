using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    public float displayTime;
    public bool needText;
    public string placeName;
    public GameObject text;
    public TextMeshProUGUI placeText;

    private CameraController cam;
    private static IEnumerator coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var tamp = cam.smoothing;
            cam.smoothing = (float)0.5;
            cam.minPos += cameraChange;
            cam.maxPos += cameraChange;
            other.transform.position += playerChange;
            cam.smoothing = tamp;
            if (needText)
            {
                if (coroutine != null) StopCoroutine(coroutine);
                coroutine = ShowTextCoroutine();
                StartCoroutine(coroutine);
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }

                text.SetActive(false);
                placeText.text = null;
            }
        }
    }

    private IEnumerator ShowTextCoroutine()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
        placeText.text = null;
    }
}