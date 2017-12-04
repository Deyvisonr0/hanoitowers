using System.Linq;

namespace HanoiTower.Domain.Entities
{
    public class Disk
    {
        public Disk(int valor)
        {
            Valor = valor;
        }

        public int Valor { get; set; }
        public override string ToString()
        {
            return string.Concat(Enumerable.Range(1, Valor).Select(n => "-"));
        }
    }
}