using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class DamageViggnetteModule : MonoBehaviour
{
    public static DamageViggnetteModule instance;

    public float damageVignetteDuration = 0.5f; // Time it takes for vignette to return to normal after damage
    public Color damageVignetteColor = Color.red; // The color of vignette during damage
    public  Volume volume;
    public Vignette vignette;

    private void Start()
    {
        instance = this;
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out vignette))
        {
          //  vignette.intensity.overrideState = true;
            vignette.color.overrideState = true;
        }
    }

    public void ApplyDamageEffect()
    {
        StartCoroutine(ChangeVignetteColor());
    }

    private IEnumerator ChangeVignetteColor()
    {
        float elapsedTime = 0f;
        Color originalColor = vignette.color.value;
       // float originalIntensity = vignette.intensity.value;

        vignette.color.value = damageVignetteColor;
        //vignette.intensity.value = 0.7f;
        while (elapsedTime < damageVignetteDuration)
        {
            vignette.color.value = Color.Lerp(damageVignetteColor, Color.black, elapsedTime / damageVignetteDuration);
           // vignette.intensity.value = Mathf.Lerp(0.7f, originalIntensity, elapsedTime / damageVignetteDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        vignette.color.value = Color.black;
       // vignette.intensity.value = originalIntensity;
    }
}
