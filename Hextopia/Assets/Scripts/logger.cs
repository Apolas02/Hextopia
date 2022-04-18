using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class logger : CivilianScript
    { 
        //CONSTRUCTORS
        public logger()
        {
            setJob("Logger");
            setLoadMaterial("Logs");
            load = 0;
            
        }
    }
}
