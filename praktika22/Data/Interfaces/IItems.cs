using praktika22.Data.Models;
using System.Collections.Generic;

namespace praktika22.Data.Interfaces
{
    public interface IItems
    {
        IEnumerable<Items> AllItems { get; } 
        int Add(Items item);
        void Update(Items item, int id);
        void Delete(int id);
        Items GetItem(int id);
    }
}