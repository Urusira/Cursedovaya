using UnityEngine;

public class Boots : PassiveItemTemplate
{
    [SerializeField] private float speedBuff;
    private float _baseSpeedBuff;
    
    protected override void Start()
    {
        _baseSpeedBuff = speedBuff;
        base.Start();
    }

    protected override void BuffApply()
    {
        base.BuffApply();
        buffs.spdBuff(speedBuff);
    }

    protected override void recalcItemStatsForCurrentLevel()
    {
        speedBuff = _baseSpeedBuff * currentItemLevel.levelCharsMultiplier;
        base.recalcItemStatsForCurrentLevel();
    }
}
