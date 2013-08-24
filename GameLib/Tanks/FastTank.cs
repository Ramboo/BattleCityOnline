﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCity.GameLib.Tanks
{
    class FastTank : AbstractTank
    {
        public FastTank(Player managingPlayer, int x, int y)
            : base(managingPlayer, x, y)
        {
            type = Type.PlayerFast;
        }
            
        public override Bullet CreateBullet()
        {
            throw new NotImplementedException();
        }
    }
}
