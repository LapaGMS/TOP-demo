// Tuodaan Unityn perustoiminnallisuudet käyttöön
using UnityEngine;

// Tuodaan UI-kirjasto, jotta Image-komponenttia voidaan käyttää
using UnityEngine.UI;

//Tarvitaan IEnumeratoria varten.
using System.Collections;

// Luokka FadeController perii MonoBehaviourin, jotta se voi toimia Unity-komponenttina
public class FadeController : MonoBehaviour
{
    // Kuinka kauan fade (häivytys) kestää sekunneissa
    public float fadeDuration = 1f;

    // Viittaus Image-komponenttiin, jota häivytetään
    private Image panelImage;

    // Tallennetaan alkuperäinen väri (sisältää myös alpha-arvon)
    private Color originalColor;

    // Viittaus käynnissä olevaan coroutineen, jotta se voidaan pysäyttää tarvittaessa
    private Coroutine currentRoutine;

    // Awake kutsutaan, kun objekti ladataan (ennen Start-metodia)
    private void Awake()
    {
        // Haetaan tästä GameObjectista Image-komponentti
        panelImage = GetComponent<Image>();

        // Tarkistetaan löytyikö Image-komponentti
        if (panelImage == null)
        {
            // Tulostetaan virhe, jos Imageä ei ole
            Debug.LogError("panel dont have a Image component");
            return;
        }

        // Tallennetaan Image-komponentin alkuperäinen väri
        // Tätä käytetään fade in -tilanteessa
        originalColor = panelImage.color;
    }

    // Julkinen metodi fade inille
    // Asettaa kohde-alpha-arvoksi alkuperäisen alfan
    public void FadeIn() => StartFade(originalColor.a);

    // Julkinen metodi fade outille
    // Asettaa kohde-alpha-arvoksi 0 (täysin läpinäkyvä)
    public void FadeOut() => StartFade(0f);

    // Käynnistää fade-prosessin haluttuun alpha-arvoon
    private void StartFade(float targetAlpha)
    {
        // Jos edellinen fade-coroutine on käynnissä, pysäytetään se
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        // Käynnistetään uusi fade-coroutine ja tallennetaan viittaus siihen
        currentRoutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    // Coroutine, joka hoitaa varsinaisen häivytyksen ajan kuluessa
    // IEnumeratot + Yield kombinaatio mahdollistaa hallitus while loopin funktiossa
    private IEnumerator FadeRoutine(float targetAlpha)
    {
        // Ajastin, joka seuraa kulunutta aikaa
        float time = 0f;

        // Tallennetaan aloitushetken alpha-arvo
        float startAlpha = panelImage.color.a;

        // Toistetaan niin kauan kuin fadeDuration ei ole täynnä
        while (time < fadeDuration)
        {
            // Lisätään kulunut aika edellisestä framesta
            time += Time.deltaTime;

            // Normalisoidaan aika välille 0–1
            float t = time / fadeDuration;

            // Haetaan nykyinen väri
            Color c = panelImage.color;

            // Lerpataan alpha aloitus- ja kohdearvon välillä
            //Lerppaaminen tarkoittaa että vaihdetaan väriä pikkuhiltaa
            // t on kulunut aika jota käytetään kertoimena (tagetAlpha - startAlpha) * t 
            //jossa t on kulunut aika nollan ja fadeDurationin väliltä.
            c.a = Mathf.Lerp(startAlpha, targetAlpha, t);

            // Asetetaan uusi väri takaisin Image-komponenttiin
            panelImage.color = c;

            // Odotetaan seuraavaan frameen
            yield return null;
        }

        // Varmistetaan lopuksi, että alpha on tarkalleen kohdearvossa
        Color final = panelImage.color;
        final.a = targetAlpha;
        panelImage.color = final;

        // Nollataan coroutine-viittaus, koska fade on valmis
        currentRoutine = null;
    }
}