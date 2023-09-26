using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InsectoSaltador
{
    public class Insecto
    {
        public Insecto()
        {
        }

        public Insecto(int patas, int cabeza, int longCabeza, int masa)
        {
            this.Patas = patas;
            this.cabeza = cabeza;
            this.longCabeza = longCabeza;
            this.Masa = masa;
        }

        public int Patas { get; set; }
        public int cabeza { get; set; }
        public int longCabeza { get; set; }
        public int Masa { get;set;}



 
    }
}
