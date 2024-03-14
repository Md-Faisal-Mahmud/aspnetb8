using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class Engine
    {
        private int _heat;

        public event Action<int> Overheat;
        public void Start()
        {
            _heat = 0;
        }

        public void Stop()
        {
            _heat = 0;
        }

        public void SpeedUp(int speed)
        {
            _heat = speed * 10;
            if (_heat > 500)
            {
                Overheat.Invoke(_heat);
            }
        }
    }
}
