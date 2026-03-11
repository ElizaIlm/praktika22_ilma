using MySql.Data.MySqlClient;
using praktika22.Data.Common;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace praktika22.Data.Mocks
{
    public class MockItems : IItems
    {
        public ICategorys ICategory = new MockCategorys();

        public IEnumerable<Items> AllItems
        {
            get
            {
                return new List<Items>()
                {
                    new Items()
                    {
                        Id = 1,
                        Name = "DEXP M5-70",
                        Description = "Крутая микроволновка",
                        Img = "https://pan-prokat.ru/upload/resize_cache/iblock/571/500_500_1/5715ba637d24b0ccd03fb23bdd6d22f3.jpg",
                        Price = 9999,
                        Category = ICategory.AllCategorys.Where(x => x.Id == 1).First()
                    },
                    new Items()
                    {
                        Id = 2,
                        Name = "DEXP M5-70",
                        Description = "Крутая мультиварка с автоматическим включением через пульт",
                        Img = "https://cdn2.cybermall.ru/images/products/003/342/660/big/26000772_1.jpg",
                        Price = 15000,
                        Category = ICategory.AllCategorys.Where(x => x.Id == 2).First()
                    }
                   
                };
            }
        }

        public int Add(Items Item)
        {
            return 1;
        }

        public void Update(Items item, int id)
        {
            
        }

        public void Delete(int id)
        {
          
        }
        public Items GetItem(int id)
        {
            return AllItems.FirstOrDefault(x => x.Id == id);
        }
    }
}