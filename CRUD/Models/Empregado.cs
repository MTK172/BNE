namespace CRUD.Models
{
    public class Empregado
    {
        public int idEmpregado { get; set; }

        public string nomeCompleto { get; set; }

        public Departamento refDepartamento { get; set; } 

        public int saldo { get; set; }

        public string fechaContrato { get; set; }
    }
}
