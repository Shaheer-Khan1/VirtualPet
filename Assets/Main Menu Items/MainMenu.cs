using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] levelButton;
    public GameObject[] buttons;
    public GameObject character;
    public GameObject backButton;
    public GameObject title;

    public void OnBackClick()
    {
        StartCoroutine(PlayOnBackClick());
    }
    public void OnStartClick()
    {
        StartCoroutine(PlayOnStartClick());
    }

    IEnumerator PlayOnBackClick()
    {
        backButton.GetComponent<Animator>().Play("SlideOut");
        StartCoroutine(ButtonPopOut());
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(CharacterSlideRight());
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(ButtonSlideIn());
        title.GetComponent<Animator>().Play("TitleLeft");
    }

    IEnumerator PlayOnStartClick()
    {
        title.GetComponent<Animator>().Play("TitleRight");
        StartCoroutine(ButtonSlideOut());
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(CharacterSlideLeft());
        yield return new WaitForSeconds(0.03f);
        StartCoroutine(ButtonPopIn());
        backButton.GetComponent<Animator>().Play("SlideIn");
    }

    IEnumerator ButtonPopIn()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            GameObject button = levelButton[i];
            button.SetActive(true);
            button.GetComponent<Animator>().Play("PopIn");
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ButtonPopOut()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            GameObject button = levelButton[i];
            button.GetComponent<Animator>().Play("PopOut");
            button.SetActive(false);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CharacterSlideLeft()
    {
        character.GetComponent<Animator>().Play("SlideLeft");
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator CharacterSlideRight()
    {
        character.GetComponent<Animator>().Play("SlideRight");
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator ButtonSlideIn()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            GameObject button = buttons[i];
            button.SetActive(true);
            button.GetComponent<Animator>().Play("SlideIn");
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ButtonSlideOut()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            GameObject button = buttons[i];
            button.GetComponent<Animator>().Play("SlideOut");
            button.SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void LoadLevel(int levelNumber = 0)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
