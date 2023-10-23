namespace TM_App01
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            Clientes clientes = new Clientes();

            clientes = new Clientes(1, "Daniel", "Saramago", "gordodaniel@gmail.com", "teste123",DateTime.Now, "Portugues", DateTime.Now);

            Console.WriteLine(clientes.ToString());
        }
    }
}