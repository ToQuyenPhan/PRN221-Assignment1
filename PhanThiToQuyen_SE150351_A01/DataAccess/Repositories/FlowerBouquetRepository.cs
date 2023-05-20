using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {
        public IEnumerable<FlowerBouquet> SearchByName(string name) => FlowerBouquetDAO.Instance.SearchByName(name);
    }
}
