using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blob : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue(int n)
    {
        bool swapin = false, bloom = false;
        if (transform.Find("Text").GetComponent<Text>().text != n.ToString())
        {
            swapin = true;
            if (transform.Find("Text").GetComponent<Text>().text != "0" && n != 0)
            {
                swapin = false;
                bloom = true;
            }
        }
        GetComponent<Image>().color = Pallete(n);
        if (swapin)
        {
            if (n == 0)
                StartCoroutine(IShrink());
            else
                StartCoroutine(ISpan());
        }
        if (bloom)
            StartCoroutine(IBloom());
        transform.Find("Text").GetComponent<Text>().text = n.ToString();
    }

    public Color32 Pallete(int n)
    {
        if (n == 2)
            return new Color32(238, 228, 218, 255);
        if (n == 4)
            return new Color32(237, 224, 200, 255);
        if (n == 8)
            return new Color32(242, 177, 121, 255);
        if (n == 16)
            return new Color32(245, 149, 99, 255);
        if (n == 32)
            return new Color32(246, 124, 95, 255);
        if (n == 64)
            return new Color32(246, 94, 59, 255);
        if (n == 128)
            return new Color32(237, 207, 114, 255);
        if (n == 256)
            return new Color32(237, 204, 97, 255);
        if (n == 512)
            return new Color32(247, 200, 80, 255);
        if (n == 1024)
            return new Color32(237, 197, 63, 255);
        if (n == 2048)
            return new Color32(237, 194, 46, 255);
        return new Color32(255, 255, 255, 255);
    }

    IEnumerator ISpan()
    {
        float f = 0;
        while (f < 1)
        {
            transform.localScale = new Vector3(f, f, f);
            f += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator IShrink()
    {
        float f = 1;
        while (f > 0)
        {
            transform.localScale = new Vector3(f, f, f);
            f -= Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(0, 0, 0);
    }
    IEnumerator IBloom()
    {
        float f = 1;
        while (f < 1.3f)
        {
            transform.localScale = new Vector3(f, f, f);
            f += Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        while (f > 1f)
        {
            transform.localScale = new Vector3(f, f, f);
            f -= Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1, 1, 1);
    }
}
