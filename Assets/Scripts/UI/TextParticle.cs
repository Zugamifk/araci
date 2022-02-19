using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextParticle : MonoBehaviour
{
    [SerializeField]
    Text m_Text;
    [SerializeField]
    float m_FloatHeight;
    [SerializeField]
    Gradient m_Fade;
    [SerializeField]
    float m_Time;

    public void Play(string text, Vector3 pos)
    {
        m_Text.text = text;
        StartCoroutine(Animate(pos));
    }

    IEnumerator Animate(Vector3 pos)
    {
        var rt = GetComponent<RectTransform>();
        var h = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta.y;
        for (var t = 0f; t < 1; t += Time.deltaTime / m_Time)
        {
            var uiPos = Camera.main.WorldToViewportPoint(pos);
            uiPos.y += m_FloatHeight/h * t;
            rt.anchorMin = uiPos;
            rt.anchorMax = uiPos;

            m_Text.color = m_Fade.Evaluate(t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
