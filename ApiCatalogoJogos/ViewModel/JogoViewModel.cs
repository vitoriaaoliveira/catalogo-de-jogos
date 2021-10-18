using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.ViewModel
{
    public class JogoViewModel
    {
        public Guid id { get; set; }
        public object Id { get; internal set; }
        public string Nome { get; set; }

        public string produtora { get; set; }
        public object Produtora { get; internal set; }
        public double Preco { get; set; }
    }
}
