using UnityEngine;

public static class Easing
{
    /// <summary>
    /// <para>にゅーにょ！にゅー</para>
    /// <para>https://easings.net/ja#easeInOutCirc</para>
    /// </summary>
    /// <param name="x">入力x 0.0f~1.0f</param>
    /// <returns>出力y</returns>
    public static float InOutCirc(float x)
    {
        return x < 0.5f
            ? (1.0f - Mathf.Sqrt(1 - Mathf.Pow(2.0f * x, 2.0f))) / 2.0f
            : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f)) + 1.0f) / 2.0f;
    }
}
