using _18._10_Best_Films_MVC.Annotations;
using System.ComponentModel.DataAnnotations;

namespace _18._10_Best_Films_MVC.Models
{
    public class Film
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле должно быыть заполненым.")]
        [Display(Name = "Название:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле должно быыть заполненым.")]
        [MinLength(3, ErrorMessage = "Слишком мало символов.")]
        [Display(Name = "Режиссёр(ы):")]
        public string Director { get; set; }

        [MinLength(4, ErrorMessage = "Слишком мало символов.")]
        [Required(ErrorMessage = "Поле должно быыть заполненым.")]
        [Display(Name = "Жанр(ы):")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Поле должно быыть заполненым.")]
        [Range(1890, 2500, ErrorMessage = "Некорректный год премьеры фильма.")]
        [Display(Name = "Год премьеры:")]
        public int ReleaseYear { get; set; }

        public string PosterPath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле должно быыть заполненым.")]
        [MinLength(30, ErrorMessage = "Слишком мало символов.")]
        [StringSeek(". ", ErrorMessage = "Неграммотно написанный текст.")]
        [Display(Name = "Описание:")]
        public string Description { get; set; }
    }
}
