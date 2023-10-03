using System.Runtime.Serialization;

namespace GalleryStorage.Models
{
    [DataContract]
    public class ErrorViewModel
    {
        [DataMember]
        public string? Message { get; set; }
        [DataMember]
        public int Status { get; set; }
    }
}