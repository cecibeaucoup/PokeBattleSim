namespace PokeBattleSim.Data.Enums
{
    /// <summary>
    /// Parameters that resolve a <see cref="MechanicTag"/>: exact weather, terrain, condition, type, etc.
    /// </summary>
    public enum ResolutionTag
    {
        EmptyTag,

        #region Movement
        GroundMovement,
        SwimMovement,
        BurrowMovement,
        LevitateMovement,
        FlyMovement,
        WallClimbMovement,
        TeleportMovement,
        #endregion

        #region Types
        NormalType,
        FightingType,
        FlyingType,
        PoisonType,
        GroundType,
        RockType,
        BugType,
        GhostType,
        SteelType,
        FireType,
        WaterType,
        GrassType,
        ElectricType,
        PsychicType,
        IceType,
        DragonType,
        DarkType,
        FairyType,
        #endregion

        #region Veils & Cloaks
        WaterVeil,
        SandVeil,
        SnowVeil,
        MistVeil,
        ToxicVeil,
        FlareVeil,
        ShockVeil,
        ShadowCloak,
        GaleCloak,
        #endregion

        #region Weather
        SunnyWeather,
        RainyWeather,
        SandyWeather,
        SnowyWeather,
        FoggyWeather,
        SmogWeather,
        HarshSunWeather,
        StormyWeather,
        WindyWeather,
        ShadowWeather,
        #endregion

        #region Terrain
        GroundTerrain,
        WaterTerrain,
        WaterPlatformsTerrain,
        DeepWaterTerrain,
        SandTerrain,
        SnowTerrain,
        GrassTerrain,
        PsychicTerrain,
        ElectricTerrain,
        MistyTerrain,
        IceTerrain,
        FlamingTerrain,
        VolcanicTerrain,
        UndergroundTerrain,
        UrbanTerrain,
        MudTerrain,
        SwampTerrain,
        #endregion

        #region Roll
        ExplodingDice,
        RerollDice,
        Threshold,
        Minimum,
        Maximum,
        Plus1,
        Plus2,
        Minus1,
        Minus2,
        #endregion

        #region Multipliers (dice and autos)
        Double,
        TwoAndHalf,
        Triple,
        ThreeAndHalf,
        Quadruple,
        Half,
        OneThird,
        Quarter,
        OneFifth,
        OneSixth,
        OneEighth,
        Zero,
        ConvertToOther,
        #endregion

        #region Checks
        PowerCheck,
        SpeedCheck,
        ToughnessCheck,
        StaminaCheck,
        AimCheck,
        EvasionCheck,
        EfficiencyCheck,
        BrawlCheck,
        #endregion

        #region Direction
        Up,
        Down
        #endregion
    }
}
