using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFont : Interactable
{
    public InventoryManager inventoryManager;

    [Space]
    public GameObject[] runes;
    public Item[] runesProperties;
    public Transform runeSpawnPoint;
    public GameEvent onGetNewRune;

    [Space]
    public float delayTime;
    private bool onDelay;
    private float onDelayTime;
    public IntVariable number;

    private bool activated;
    private GameObject activeRune;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Delay());
    }

    public void GetResource()
    {
        if (onDelay)
            return;

        UpdateInventory();

        ActivateCanvas();
    }

    IEnumerator Delay()
    {
        onDelay = true;

        while (onDelayTime <= delayTime)
        {
            onDelayTime += Time.deltaTime;
            yield return null;
        }

        SpawnNewRune();

        onDelayTime = 0;
        onDelay = false;
    }

    private void UpdateInventory()
    {
        onGetNewRune.Raise();
        inventoryManager.AddItem(runesProperties[number.Value]);
        number.Value = -1;
        Destroy(activeRune);
        activeRune = null;
        StartCoroutine(Delay());
    }

    private void SpawnNewRune()
    {
        number.Value = Random.Range(0, runes.Length);

        activeRune = Instantiate(runes[number.Value], runeSpawnPoint.position, Quaternion.identity);

        ActivateCanvas();
    }

    public override void ActivateInteraction()
    {
        base.ActivateInteraction();
        activated = true;
        ActivateCanvas();
    }

    public override void DesactivateInteraction()
    {
        base.DesactivateInteraction();
        activated = false;
    }

    private void ActivateCanvas()
    {
        if (!activated)
            return;

        if (activeRune)
            canvas.SetActive(true);
        else
            canvas.SetActive(false);
    }

}
