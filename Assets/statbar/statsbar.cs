using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class statsbar : MonoBehaviour {

    public RectTransform powerrect = null;
    public RectTransform shieldrect = null;
    public RectTransform health0rect = null;
    public RectTransform health1rect = null;

    public float power = 0f;  // 0-1
    public float shield = 0f; // 0-1
    public float health = 0f; // 0-1

    // Reference to the Image component in the "bottom mask" GameObject
    public UnityEngine.UI.Image healthImage;

    void Update()
    {
        power = Mathf.Clamp01(power);
        shield = Mathf.Clamp01(shield);
        health = Mathf.Clamp01(health);

        powerrect.localRotation = Quaternion.AngleAxis(130f * (1f - power), Vector3.forward);
        shieldrect.localRotation = Quaternion.AngleAxis(130f * (1f - shield), Vector3.forward);
        health0rect.localRotation = Quaternion.AngleAxis(122f * (1f - health), Vector3.forward);
        health1rect.localRotation = Quaternion.AngleAxis(95f * (1f - health), Vector3.forward);

        // Set the color based on the health value
        UpdateHealthColor();
    }

    void UpdateHealthColor()
    {
        if (health >= 0.7f)
        {
            healthImage.color = Color.green;
        }
        else if (health >= 0.3f)
        {
            healthImage.color = Color.yellow;
        }
        else
        {
            healthImage.color = Color.red;
        }
    }

    public void DecreaseHealth()
    {
        health -= 0.1f;
        health = Mathf.Clamp01(health); // Ensure health stays in the range [0, 1]
    }

    public void KnightHit()
    {
        health -= 0.3f;
        health = Mathf.Clamp01(health);
    }
}





