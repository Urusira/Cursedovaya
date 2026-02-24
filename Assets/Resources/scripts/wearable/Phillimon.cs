using UnityEngine;

public class Phillimon : PassiveItemTemplate
{
    [SerializeField] private float immortBuff;
    private float _baseImmortBuff;
    
    protected override void Start()
    {
        _baseImmortBuff = immortBuff;
        base.Start();
    }

    protected override void BuffApply()
    {
        base.BuffApply();
        buffs.imrtBuff(immortBuff);
    }

    protected override void recalcItemStatsForCurrentLevel()
    {
        immortBuff = _baseImmortBuff * currentItemLevel.levelCharsMultiplier;
        base.recalcItemStatsForCurrentLevel();
    }
}
