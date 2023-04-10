using System.ComponentModel.DataAnnotations;

namespace DataForwardingWeb.DTO.Model
{
    public class RequestFitherModel
    {
        [Required(ErrorMessage = "Укажите минимальное значение")]
        [MinLength(0, ErrorMessage = "Значение не может быть отрицательное")]
        public string MinValue { get; set; }

        [Required(ErrorMessage = "Укажите максимальное значение")]
        [MinLength(8, ErrorMessage = "Минимальная длинна значения 8")]
        public string MaxValue { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор тэга")]
        public string TagId { get; set; }

    }
}
