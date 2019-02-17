using System.ComponentModel.DataAnnotations;

namespace Projeto.Livaria.Api.Models
{
    public class LivroModel
    {
        [Required(ErrorMessage = "Por favor preencha Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor preencha nome")]
        [StringLength(30, ErrorMessage = "Nome deve ter no maximo 30 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor preencha autor")]
        [StringLength(30, ErrorMessage = "Autor deve ter no maximo 30 caracteres")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Por favor preencha ano")]
        public int Ano { get; set; }
    }

    public class LivroModelCadastrar
    {
        [Required(ErrorMessage = "Por favor preencha nome")]
        [StringLength(30,ErrorMessage ="Nome deve ter no maximo 30 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor preencha autor")]
        [StringLength(30, ErrorMessage = "Autor deve ter no maximo 30 caracteres")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "Por favor preencha ano")]
        public int Ano { get; set; }

    }
}
