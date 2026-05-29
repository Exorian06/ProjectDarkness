using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDarkness
{

    public enum CellEventType
    {
        None,
        City,
        Merchant,
        Battle,
        Upgrade,
        Discovery,
        Quest
    }


    public class MapCell
    {
        public Image CellImage { get; set; }

        public Func<Task> OnEnter { get; set; }

        public CellEventType CellType { get; set; } = CellEventType.None;

        public string CellId { get; set; } 
    }
}
