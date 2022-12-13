using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(P3dPaintable))]
[RequireComponent(typeof(P3dMaterialCloner))]
[RequireComponent(typeof(P3dPaintableTexture))]

public class PaintableObject : Interactable
{
    #region Components
    [SerializeField] private P3dPaintable _paintable = null;

    #endregion

    public override bool OnInteract(bool successed = false) {
        _paintable.enabled = true;
        return base.OnInteract(true);
        //TODO: Drawķ���� ��ȯ
    }

    public override bool OffInteract(bool successed  = false) {
        _paintable.enabled = false;
        return base.OffInteract(true);
        //TODO: PlayerFollowķ���� ��ȯ
    }

    protected override void OnPlayerTriggered() {
        base.OnPlayerTriggered();
    }

    protected override void OnPlayerUntriggered() {
        base.OnPlayerUntriggered();
    }
}
