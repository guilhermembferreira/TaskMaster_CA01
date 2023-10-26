namespace TM_App01
{
    internal class Program
    {                
        static void Main(string[] args)
        {
            Trabalhadores clientes = new Trabalhadores();

            clientes = new Trabalhadores(1, "Daniel", "Saramago", "gordodaniel@gmail.com", "teste123",DateTime.Now, "Portugues", DateTime.Now);

            Console.WriteLine(clientes.ToString());
        }
    }
}