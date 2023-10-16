using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
    [SerializeField] GameObject aboutText;
    [SerializeField] GameObject levelTitleText;
    [SerializeField] GameObject backgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        backgroundImage.SetActive(true);
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
        yield return new WaitForSeconds(5f);
        aboutText.SetActive(false);
        levelTitleText.SetActive(false);
        backgroundImage.SetActive(false);
    }
}
