using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
  private Button button;
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;
    private float targetScaleFactor = 1.2f; // Increase size by 20%
    private float scaleSpeed = 5f; // Speed at which the button scales
	public AudioSource audioSource;
    private void Start()
    {

			button = GetComponent<Button>();
			originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            StopScaleCoroutine();
            scaleCoroutine = StartCoroutine(ScaleButton(originalScale * targetScaleFactor));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable)
        {
            StopScaleCoroutine();
            scaleCoroutine = StartCoroutine(ScaleButton(originalScale));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button.interactable)
        {
			if(audioSource)
			audioSource.Play();
            StopScaleCoroutine();
            scaleCoroutine = StartCoroutine(ScaleButton(originalScale * 0.8f)); // Decrease size by 20%
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            StopScaleCoroutine();
            scaleCoroutine = StartCoroutine(ScaleButton(originalScale));
        }
    }

    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void StopScaleCoroutine()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
    }
}