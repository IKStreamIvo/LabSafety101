using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Container : MonoBehaviour
{
    [SerializeField] private int _waistId;
    [SerializeField] private GameController.PickupType acceptsType;

    private bool targeted;
    private Color defaultColor;
    [SerializeField] private Color targetedColor;
    [SerializeField] private Renderer render;

    private TextMeshPro phDisplay;
    private bool prevTargetState;


    private float t = 0.0f;

    private bool explode = false;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject darkRoom;
    [SerializeField] private Transform player;

    private bool explodeConfetti = false;
    [SerializeField] private GameObject confetti;

    private void Start()
    {
        defaultColor = render.material.color;

        phDisplay = GetComponentInChildren<TextMeshPro>();
        phDisplay.SetText(_waistId.ToString());
    }

    public void Update()
    {
        if (targeted)
        {
            Material mat = render.material;
            mat.color = targetedColor;
            render.material = mat;
            Debug.Log("Targeted");
        }
        else
        {
            Material mat = render.material;
            mat.color = defaultColor;
            render.material = mat;
        }
        prevTargetState = targeted;
        targeted = false;

        /*
        if (explode)
        {
            Renderer rend = darkRoom.GetComponent<Renderer>();
            rend.material.SetFloat("_Metallic", Mathf.Lerp(0, 1, t));
            t += 0.5f * Time.deltaTime;
        }
        else
        {
            Renderer rend = darkRoom.GetComponent<Renderer>();
            rend.material.SetFloat("_Metallic", 0);
        }*/
    }

    public void PlaceObject(GameObject target)
    {
        InsideGlass liquid = target.GetComponentInChildren<InsideGlass>();
        ph phValue = liquid.phValue;
        if (phValue.CheckPHWaist(_waistId))
        {
            Debug.Log("Correct!");
            StartCoroutine(Confetti());
        }
        else
        {
            Debug.Log("Incorrect...");
            StartCoroutine(Exploding());
        }
        Destroy(target);
    }

    public bool TargetHit(GameController.PickupType type)
    {
        if (acceptsType != type)
            targeted = false;
        else
            targeted = true;

        return targeted;
    }

    private IEnumerator Exploding()
    {
        GameController._.Lose();
        GameObject currentExplosion = Instantiate(explosion, transform);
        GameObject room = Instantiate(darkRoom, player);
        //darkRoom.SetActive(true);
        explode = true;

        yield return new WaitForSeconds(5f);
        //darkRoom.SetActive(false);
        t = 0.0f;
        explode = false;
        Destroy(currentExplosion);
        Destroy(room);
        SceneManager.LoadScene(1);
    }

    private IEnumerator Confetti()
    {
        GameController._.Win();
        GameObject currentConfetti = Instantiate(confetti, transform);
        yield return new WaitForSeconds(7f);
        Destroy(currentConfetti);
        SceneManager.LoadScene(1);
    }
}
