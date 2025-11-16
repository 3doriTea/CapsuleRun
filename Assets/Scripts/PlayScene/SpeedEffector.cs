using System.Collections.Generic;
using UnityEngine;

public class SpeedEffector : MonoBehaviour
{
    [SerializeField]
    private Transform effectRoot;
    [SerializeField]
    private GameObject playerModel;

    const float IntervalTime = 0.1f;
    const float EffectTime = 0.5f;

    private float popTimeLeft = 0.0f;

    List<Transform> effects = new List<Transform>();
    List<float> timeLefts = new List<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        popTimeLeft -= Time.deltaTime;
        if (popTimeLeft <= 0.0f)
        {
            popTimeLeft += IntervalTime;

            GameObject effect = Instantiate(playerModel, effectRoot, true);

            effect.transform.localScale = new Vector3(effect.transform.localScale.x, effect.transform.localScale.y, 0.1f);

            effects.Add(effect.transform);
            timeLefts.Add(EffectTime);
        }

        for (int i = 0; i < effects.Count; i++)
        {
            timeLefts[i] -= Time.deltaTime;
            float value = timeLefts[i] / EffectTime;
            effects[i].localScale = new Vector3(value, value, 0.1f);
            if (timeLefts[i] <= 0.0f)
            {
                Destroy(effects[i].gameObject);
                effects.RemoveAt(i);
                timeLefts.RemoveAt(i);
            }
        }
    }
}
