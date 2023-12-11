﻿using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerData
    {
        public float balance;
        public float damage;
        public List<UpgradesData> upgradesData = new();
    }
}