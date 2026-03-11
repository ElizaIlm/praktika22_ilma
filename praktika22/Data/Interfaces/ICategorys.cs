using praktika22.Data.Models;
using System.Collections.Generic;

namespace praktika22.Data.Interfaces
{
    public interface ICategorys
    {
        IEnumerable<Categorys> AllCategorys { get; } 
    }
}