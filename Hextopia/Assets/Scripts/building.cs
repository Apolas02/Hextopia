using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class building : MonoBehaviour
    {
        private int health;
        private int load;
        private int maxLoad;
        private string loadMaterial;
        private string target;

        //Location Values
        private Vector3 location;
        //Target Location Values
        private Vector3 targetLocation;

        //Set Values
        public void setLocation(Vector3 l)
        {
            location = l;
        }
        public void setHealth(int h)
        {
            health = h;
        }
        public void setLoad(int l)
        {
            load = l;
        }
        public void setMaxLoad(int ml)
        {
            maxLoad = ml;
        }
        public void setLoadMaterial(string lm)
        {
            loadMaterial = lm;
        }
        public void setTarget(string t)
        {
            target = t;
        }


        //Get Values

        public int getHealth()
        {
            return health;
        }
        public int getLoad()
        {
            return load;
        }
        public int getMaxLoad()
        {
            return maxLoad;
        }
        public string getLoadMaterial()
        {
            return loadMaterial;
        }
        public string getTarget()
        {
            return target;
        }



        //CONSTRUCTORS
        public building()
        {
            health = 10;
            load = 10;
            maxLoad = 10;
        }
        public building(int h, int l, int ml)
        {
            health = h;
            load = l;
            maxLoad = ml;
        }


    }
}
