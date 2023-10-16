using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
    [SerializeField] GameObject aboutText;
    [SerializeField] GameObject levelTitleText;

    // Start is called before the first frame update
    void Start()
    {
        aboutText.SetActive(true);
        levelTitleText.SetActive(true);

        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowText()
    {
        yield return new WaitForSeconds(10f);
        aboutText.SetActive(false);
        levelTitleText.SetActive(false);
    }
}
