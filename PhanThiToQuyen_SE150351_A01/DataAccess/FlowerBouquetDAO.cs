using BusinessObject.Context;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FlowerBouquetDAO
    {
        private static FlowerBouquetDAO instance = null;
        private static readonly object instanceLock = new object();

        private FlowerBouquetDAO()
        {
        }

        public static FlowerBouquetDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) instance = new FlowerBouquetDAO();
                    return instance;
                }
            }
        }
        public IEnumerable<FlowerBouquet> SearchByName(string name)
        {
            using (var context = new FUFlowerBouquetManagement())
            {
                return context.FlowerBouquets.Where(c => c.FlowerBouquetName.Contains(name)).ToList();
            }
        }
    }
}
