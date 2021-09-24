using System;

namespace Almeida.Entity
{
    public class eBase
    {
        public int? ID { get; set; }
        public DateTime? DataInclusão { get; set; }
        public DateTime? DataAlteração { get; set; }
        public bool Fl_Ativo { get; set; }
        public eMessage Mensagem { get; set; }
    }
}
