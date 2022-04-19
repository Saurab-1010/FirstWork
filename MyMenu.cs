using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFirst.Models
{
    public class MyMenu
    {
        public static List<tblCategory> GetMenus()
        {
            using (var context = new ProjectEntities())
            {
                return context.tblCategories.ToList();
            }
        }
      
    }
}