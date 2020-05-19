using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    class Chess
    {
        public const int SIZE = 30;
        private int _dong;
        private int _cot;
        private int _player;

        public int Dong { get => _dong; set => _dong = value; }
        public int Cot { get => _cot; set => _cot = value; }
        public int Player { get => _player; set => _player = value; }

        public Chess(int dong, int cot, int player)
        {
            Dong = dong;
            Cot = cot;
            Player = player;
        }
    }
}
