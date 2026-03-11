using MySql.Data.MySqlClient;
using praktika22.Data.Common;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace praktika22.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categorys> Categorys = new DBCategory().AllCategorys;

        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT * FROM Shop.Items ORDER BY `Name`;", MySqlConnection);

                while (ItemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? "" : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? "" : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? "" : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Category = ItemsData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == ItemsData.GetInt32(5)).FirstOrDefault()
                    });
                }

                MySqlConnection.Close();
                return items;
            }
        }

        public int Add(Items Item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery(
                $"INSERT INTO `Items` (`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}', '{Item.Description}', '{Item.Img}', {Item.Price}, {Item.Category.Id});",
                MySqlConnection);
            MySqlConnection.Close();

            int IdItem = -1;
            MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader MySqlDataReaderItem = Connection.MySqlQuery(
                $"SELECT `Id` FROM `Items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Category.Id};",
                MySqlConnection);

            if (MySqlDataReaderItem.HasRows)
            {
                MySqlDataReaderItem.Read();
                IdItem = MySqlDataReaderItem.GetInt32(0);
            }

            MySqlConnection.Close();
            return IdItem;
        }

        public void Update(Items Item, int id)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery(
                $"UPDATE `Items` SET `Name`='{Item.Name}',`Description`='{Item.Description}',`Img`='{Item.Img}'," +
                $"`Price`={Item.Price},`IdCategory`={Item.Category.Id} WHERE `Id`={id}",
                MySqlConnection);
            MySqlConnection.Close();
        }

        public void Delete(int id)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($"DELETE FROM `Items` WHERE `Id`={id}", MySqlConnection);
            MySqlConnection.Close();
        }

        public Items GetItem(int id)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader ItemData = Connection.MySqlQuery($"SELECT * FROM Shop.Items WHERE Id = {id};", MySqlConnection);

            Items item = null;

            if (ItemData.Read())
            {
                item = new Items()
                {
                    Id = ItemData.IsDBNull(0) ? -1 : ItemData.GetInt32(0),
                    Name = ItemData.IsDBNull(1) ? "" : ItemData.GetString(1),
                    Description = ItemData.IsDBNull(2) ? "" : ItemData.GetString(2),
                    Img = ItemData.IsDBNull(3) ? "" : ItemData.GetString(3),
                    Price = ItemData.IsDBNull(4) ? -1 : ItemData.GetInt32(4),
                    Category = ItemData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == ItemData.GetInt32(5)).FirstOrDefault()
                };
            }

            MySqlConnection.Close();
            return item;
        }
    }
}