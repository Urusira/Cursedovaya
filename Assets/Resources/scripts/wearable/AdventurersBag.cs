using UnityEngine;

public class AdventurersBag : PassiveItemTemplate
{
    [SerializeField] private int invCap;
    private int _baseInvCap;
    
    protected override void Start()
    {
        _baseInvCap = invCap;
        base.Start();
    }

    protected override void BuffApply()
    {
        base.BuffApply();
        buffs.invBuff(invCap);
    }

    protected override void recalcItemStatsForCurrentLevel()
    {
        invCap = _baseInvCap + (int)currentItemLevel.levelCharsMultiplier;
        base.recalcItemStatsForCurrentLevel();
    }
}
