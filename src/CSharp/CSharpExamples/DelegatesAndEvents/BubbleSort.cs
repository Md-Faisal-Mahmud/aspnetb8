using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public class BubbleSort<T>
    {
        public IList<T> Sort(IList<T> list, Func<T, T, int> compare)
        {
            T temp;
            for (int j = 0; j <= list.Count - 2; j++)
            {
                for (int i = 0; i <= list.Count - 2; i++)
                {
                    if (compare == null)
                        compare = (T a, T b) => 0;

                    if (compare(list[i], list[i + 1]) > 0)
                    {
                        temp = list[i + 1];
                        list[i + 1] = list[i];
                        list[i] = temp;
                    }
                }
            }

            return list;
        }
    }
}
