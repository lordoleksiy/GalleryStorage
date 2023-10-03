using System.ComponentModel.DataAnnotations;

namespace GalleryStorage.Models
{
    public class StorageListModel
    {
        public IEnumerable<string> Names { get; set; }
        [Required(ErrorMessage = "Поле 'Ім'я контейнера' обязательно для заполнения.")]
        [MinLength(5, ErrorMessage = "Минимальная длина 'Ім'я контейнера' - 5 символов.")]
        public string NewContainerName { get; set; }
        public StorageListModel() 
        {
            Names = new List<string>();
            NewContainerName = string.Empty;
        }
        public StorageListModel(IEnumerable<string> names) 
        {
            Names = names;
            NewContainerName = "";
        }
        public bool IsCorrectNewName()
        {
            return !Names.Contains(NewContainerName);
        }
    }
}
