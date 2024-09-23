using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int skinId;
    [SerializeField] private Animator anim;

    public void SelectSKin()
    {
        SkinManager.instance.SetSkin(skinId);
    }

    public void NextSkin()
    {
        skinId++;

        if(skinId >= anim.layerCount)
            skinId = 0;

        UpdateSkinDisplay();
    }

    public void PreviousSkin()
    {
        skinId--;

        if(skinId < 0)
            skinId = anim.layerCount - 1;
    
        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {
        for(int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(skinId, 1);
    }
}
