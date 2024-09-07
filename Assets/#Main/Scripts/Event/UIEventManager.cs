using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIEventManager : MonoBehaviour
{
    [Header("Variable")]
    public string targetDialog;
    public float delay = 0.05f;
    private Coroutine dialogCoroutine;
    private bool isComplete = false;

    [Header("UI")]
    public Image imageArrowLeft;
    public Image imageArrowRight;

    public Image imageMakInan;
    public Image imageNPC;

    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI nameText;
    public List<TextMeshProUGUI> listTextChoice;
    public List<Button> listButtonChoice;

    [Header("Event")]
    public UnityEvent onDialogDone;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isComplete)
        {
            SkipDialog();
        }
    }

    public void SetUpDialog(string dialog, string name, DialogPerson dialogPerson, Sprite spMakInan, Sprite spNPC)
    {
        targetDialog = dialog;

        imageMakInan.sprite = spMakInan;
        imageNPC.sprite = spNPC;

        switch (dialogPerson)
        {
            case DialogPerson.NARATOR:
                nameText.text = name;
                break;

            case DialogPerson.MAK_INAN:
                nameText.text = name;
                break;

            case DialogPerson.NPC:
                nameText.text = name;
                break;

            case DialogPerson.CHOICE:
                nameText.text = "";
                break;
            default:
                break;
        }

        SetUIArrow(dialogPerson);

        PlayDialog();
    }

    public void PlayDialog()
    {
        dialogCoroutine = StartCoroutine(TampilkanDialogPerHuruf(targetDialog));
    }

    public void ClickDialog()
    {
        if (!isComplete)
        {
            SkipDialog();
        }
        else
        {
            onDialogDone?.Invoke();
        }
    }

    public void SkipDialog()
    {
        StopCoroutine(dialogCoroutine);
        dialogText.text = targetDialog;
        isComplete = true;
    }

    IEnumerator TampilkanDialogPerHuruf(string kalimat)
    {
        dialogText.text = "";
        isComplete = false;

        foreach (char huruf in kalimat)
        {
            dialogText.text += huruf;
            yield return new WaitForSeconds(delay);
        }

        isComplete = true;
    }


    private void SetUIArrow(DialogPerson type)
    {
        switch (type)
        {
            case DialogPerson.MAK_INAN:
                imageArrowLeft.gameObject.SetActive(true);
                imageArrowRight.gameObject.SetActive(false);
                break;
            case DialogPerson.NPC:
                imageArrowLeft.gameObject.SetActive(false);
                imageArrowRight.gameObject.SetActive(true);
                break;

            case DialogPerson.NARATOR:
            case DialogPerson.CHOICE:
                imageArrowLeft.gameObject.SetActive(false);
                imageArrowRight.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
