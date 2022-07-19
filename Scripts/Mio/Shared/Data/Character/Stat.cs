using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Mio.Data
{
    [System.Serializable]
    public struct FStatRange<T>
    {
        public T Min;
        public T Max;
        public T Current;
    }

    public struct FBaseStat
    {
        public FStatRange<int> Strength;
        public FStatRange<int> Intelligence;
        public FStatRange<int> Dexterity;
        public FStatRange<int> Vitality;
    }

    public interface IComputedStat
    {

    }

    public struct FComputedStat : IComputedStat
    {

    }
}
