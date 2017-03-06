using System.Collections.Generic;
using ORM;

namespace DAL
{
    public class CarsComparer : IEqualityComparer<Car>
    {
        public bool Equals(Car x, Car y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Car obj)
        {
            return obj.Id;
        }
    }
}
