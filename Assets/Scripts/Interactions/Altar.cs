using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Altar : MonoBehaviour, IInteractable
{
    public VisualEffect vfxRay;

    [SerializeField] private Material PP_BeamDistort;
    private string _blindAmmountName = "_BlindAmmount";
    private string _hueAmmountName = "_HueAmmount";
    private string _ColorMaskName = "_ColorToMask";
    private float _effectDuration = 2f;

    public Transform gemPlace;

    public string[] lines;

    public int gemAmmount = 0;

    private GameObject actualGem;

    private Color _maskColor;

    private void Start()
    {
        vfxRay.Stop();
        PP_BeamDistort.SetFloat(_blindAmmountName, 0f);
        PP_BeamDistort.SetFloat(_hueAmmountName, 0f);
    }

    public string GetDescription()
    {
        return "Interact";
    }

    public void Interact()
    {
        List<string> list;
        //IF tengo piedra no dialogo, ELSE, dialogo x * cantpiedras
        if (PlayerInventory.Instance.itemInHand != null)
        {
            if (PlayerInventory.Instance.itemInHand.type == ItemType.Gem)
            {
                gemAmmount++;
                list = new List<string> { lines[gemAmmount] };
                Debug.Log(gemAmmount);
                _maskColor = PlayerInventory.Instance.itemInHand.prefab.GetComponent<Gem>().maskColor;
                StartRayEffect();
            }
            else
            {
                list = new List<string> { lines[0] };
            }
        }
        else
        {
            list = new List<string> { lines[0] };
        }
        
        Dialogue.Instance.StartDialogue(list, true);
    }

    void StartRayEffect()
    {
        StartCoroutine(RayInteraction());
    }

    IEnumerator RayInteraction()
    {
        if(actualGem != null)
        {
            Destroy(actualGem.gameObject);
        }

        actualGem = Instantiate(PlayerInventory.Instance.itemInHand.prefab, gemPlace.position, Quaternion.identity);
        PlayerInventory.Instance.StoreCurrentItem();
        yield return new WaitForSeconds(3f);
        vfxRay.Play();
        yield return new WaitForSeconds(1.5f);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime/ _effectDuration;
            PP_BeamDistort.SetFloat(_blindAmmountName, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        PP_BeamDistort.SetColor(_ColorMaskName, _maskColor);
        PP_BeamDistort.SetFloat(_hueAmmountName, 1f);
        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / _effectDuration;
            PP_BeamDistort.SetFloat(_blindAmmountName, t);
            yield return null;
        }

    }
}
