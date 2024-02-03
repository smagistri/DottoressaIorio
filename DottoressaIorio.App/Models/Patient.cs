using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DottoressaIorio.App.Models
{
    public class Patient : IDataHandler
    {
        public int PatientId { get; set; }

        public string? Title { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Il Cognome è obbligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La Data di Nascita è obbligatoria.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "2099-12-31", ErrorMessage = "La data di nascita deve essere compresa tra il 1900/01/01 e il 2099/12/31.")]
        public DateTime? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? Gender { get; set; }

        [RegularExpression(@"^\+?(\d{1,4})?[-. ]?(\(\d{1,4}\))?[ .-]?(\d{1,10})$", ErrorMessage = "Il formato del numero di telefono non è valido.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "L'indirizzo email è obbligatorio.")]
        [EmailAddress(ErrorMessage = "Indirizzo email non valido.")]
        public string Email { get; set; }

        public List<Therapy> Therapies { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? EditDate { get; set; }

        public bool Deleted { get; set; }
    }
}