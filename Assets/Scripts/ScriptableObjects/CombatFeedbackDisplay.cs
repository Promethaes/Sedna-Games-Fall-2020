using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CombatFeedbackDisplay : MonoBehaviour
{
    public CombatFeedback feedback;
    Material originalMaterial;
    SkinnedMeshRenderer skinnedMeshRenderer = null;
    public GameObject postProcessVolume;
    Vignette vignetteComponent = null;
    float originalVignetteIntensity = 0.0f;
    public float vignetteDecayStepSize = 0.01f;

    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        if (postProcessVolume != null)
        {
            var volume = postProcessVolume.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out vignetteComponent);
            originalVignetteIntensity = vignetteComponent.intensity;
        }

        FindSkinnedMeshRenderer();
    }

    private void Update() {
        if(test){
            test = false;
            OnTakeDamage();
        }
    }

    bool FindSkinnedMeshRenderer()
    {
        //separate statements because unity was angry at me
        if (skinnedMeshRenderer != null)
        {
            if (skinnedMeshRenderer.gameObject.activeInHierarchy)
                return true;
        }

        skinnedMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>(includeInactive: false);

        if (!skinnedMeshRenderer)
        {
            Debug.LogError("Combat Feedback of " + gameObject.name + " could not find the skinned mesh renderer!");
            enabled = false;
            return false;
        }

        if (!skinnedMeshRenderer.sharedMaterial)
        {
            Debug.LogError("Skinned Mesh Renderer for " + gameObject.name + " does not have a proper material!");
            return false;
        }
        originalMaterial = skinnedMeshRenderer.sharedMaterial;

        return true;
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(feedback.flickerDuration);
        skinnedMeshRenderer.sharedMaterial = originalMaterial;
        if (postProcessVolume != null)
            postProcessVolume.SetActive(false);
    }

    IEnumerator VignetteFade()
    {
        if (postProcessVolume == null || vignetteComponent == null)
            yield return null;

        while (vignetteComponent.intensity > 0.0f)
        {
            vignetteComponent.intensity.value -= vignetteDecayStepSize;
            yield return new WaitForSeconds(vignetteDecayStepSize);
        }

        vignetteComponent.intensity.value = originalVignetteIntensity;
    }

    public void OnTakeDamage()
    {
        if (!FindSkinnedMeshRenderer())
            return;
        skinnedMeshRenderer.sharedMaterial = feedback.flickerMaterial;
        StartCoroutine("ResetMaterial");

        if (postProcessVolume != null && !gameObject.name.Contains("REMOTE"))
        {
            postProcessVolume.SetActive(true);
            StartCoroutine("VignetteFade");
        }
    }
}
