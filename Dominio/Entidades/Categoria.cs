using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        public int Id {get; set;}

        public string Nome {get; set;}

        public bool PermiteEstoque {get; set;}
    }
}